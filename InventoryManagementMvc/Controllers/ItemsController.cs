using InventoryManagementMvc.Data;
using InventoryManagementMvc.Models;
using InventoryManagementMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementMvc.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context)
        {
            _context= context;
        }

        public async Task<IActionResult> Index()
        {
            var item=await _context.Items.Include(s=>s.SerialNumber)
                                         .Include(c=>c.Category)
                                         .Include(ic => ic.ItemClients)
                                         .ThenInclude(c=>c.Client)
                                          .ToListAsync();
            return View(item);
        }
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", vm.CategoryId);
                return View(vm);
            }

            var item = new Item
            {
                Name = vm.Name,
                Price = vm.Price,
                CategoryId = vm.CategoryId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync(); // para obtener item.Id

            // Serial (1:1)
            if (!string.IsNullOrWhiteSpace(vm.SerialNumber))
            {
                _context.SerialNumbers.Add(new SerialNumber
                {
                    Name = vm.SerialNumber.Trim(),
                    ItemId = item.Id
                });
            }

            // Client (crear o asociar) + vínculo N:N
            if (!string.IsNullOrWhiteSpace(vm.ClientName))
            {
                var clientName = vm.ClientName.Trim();

                var existingClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Name == clientName);

                var client = existingClient ?? new Client { Name = clientName };

                if (existingClient == null)
                    _context.Clients.Add(client);

                await _context.SaveChangesAsync(); // para obtener client.Id si era nuevo

                // crear link ItemClient (si no existe)
                var linkExists = await _context.ItemClients
                    .AnyAsync(ic => ic.ItemId == item.Id && ic.ClientId == client.Id);

                if (!linkExists)
                {
                    _context.ItemClients.Add(new ItemClient
                    {
                        ItemId = item.Id,
                        ClientId = client.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")] Item item)
        //{
        //    if (ModelState.IsValid) {
        //        _context.Items.Add(item);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(item);
        //}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        //    var item = await _context.Items.FirstOrDefaultAsync(x=>x.Id==id);
        //    if (item == null) return NotFound();
        //    return View(item);
        //}
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items
                .Include(i => i.SerialNumber)
                .Include(i => i.ItemClients)
                    .ThenInclude(ic => ic.Client)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            var vm = new EditItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.CategoryId,
                SerialNumber = item.SerialNumber?.Name,
                Clients = string.Join(", ",
                    item.ItemClients.Select(ic => ic.Client!.Name))
            };

            ViewData["Categories"] =
                new SelectList(_context.Categories, "Id", "Name", item.CategoryId);

            return View(vm);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price,CategoryId")] Item item)
        //{
        //    if (ModelState.IsValid) {
        //        _context.Update(item);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(item);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] =
                    new SelectList(_context.Categories, "Id", "Name", vm.CategoryId);
                return View(vm);
            }

            var item = await _context.Items
                .Include(i => i.SerialNumber)
                .Include(i => i.ItemClients)
                .FirstOrDefaultAsync(i => i.Id == vm.Id);

            if (item == null) return NotFound();

            // 🔹 Item
            item.Name = vm.Name;
            item.Price = vm.Price;
            item.CategoryId = vm.CategoryId;

            // 🔹 Serial
            if (!string.IsNullOrWhiteSpace(vm.SerialNumber))
            {
                if (item.SerialNumber == null)
                {
                    item.SerialNumber = new SerialNumber
                    {
                        Name = vm.SerialNumber
                    };
                }
                else
                {
                    item.SerialNumber.Name = vm.SerialNumber;
                }
            }
            else if (item.SerialNumber != null)
            {
                _context.SerialNumbers.Remove(item.SerialNumber);
            }

            // 🔹 Clients (N:N con input)
            _context.ItemClients.RemoveRange(item.ItemClients);

            if (!string.IsNullOrWhiteSpace(vm.Clients))
            {
                var clientNames = vm.Clients
                    .Split(',')
                    .Select(c => c.Trim())
                    .Where(c => c != "");

                foreach (var name in clientNames)
                {
                    var client = await _context.Clients
                        .FirstOrDefaultAsync(c => c.Name == name);

                    if (client == null)
                    {
                        client = new Client { Name = name };
                        _context.Clients.Add(client);
                        await _context.SaveChangesAsync();
                    }

                    _context.ItemClients.Add(new ItemClient
                    {
                        ItemId = item.Id,
                        ClientId = client.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item !=null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

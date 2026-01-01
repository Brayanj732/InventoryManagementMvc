using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementMvc.ViewModels
{
    public class CreateItemViewModel
    {
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public int? CategoryId { get; set; }

        // NUEVO
        public string? SerialNumber { get; set; }
        // Nuevo: input para crear/asociar cliente
        public string? ClientName { get; set; }
    }
}

namespace InventoryManagementMvc.ViewModels
{
    public class EditItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public int? CategoryId { get; set; }

        // Serial
        public string? SerialNumber { get; set; }

        // Clientes como TEXTO
        // Ej: "Brayan, David, Carlos"
        public string? Clients { get; set; }
    }
}

namespace InventoryManagementMvc.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemClient>? ItemClients { get; set; }
    }
}

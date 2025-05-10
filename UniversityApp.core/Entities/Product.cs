namespace UniversityApp.Core.Entities
{
    public class Product
    {
        public int Id { get; set; } // EF Core primary key by convention
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign key
    }
}

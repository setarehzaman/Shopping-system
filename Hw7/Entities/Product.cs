using Hw7.Enums;

namespace Hw7.Entities
{
    public class Product
    {
        public Product()
        { 
            Quantity = 10;
            Count = 1;
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public CategoryEnum Category { get; set; }


    }
}

using Store.DTOs.Category;

namespace Store.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public CategoryDto Category { get; set; }
    }
}

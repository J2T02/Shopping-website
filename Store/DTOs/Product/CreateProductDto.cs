namespace Store.DTOs.Product
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}

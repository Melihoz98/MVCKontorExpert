namespace MVCKontorExpert.Models
{
    public class Product
    {
        public Product() { }
        public Product(string name, string description, string brand, decimal price, int stockQuantity, string color, string dimensions, int categoryID, bool isUsed) 
        {
            Name = name;
            Description = description;
            Brand = brand;
            Price = price;
            StockQuantity = stockQuantity;
            Color = color;
            Dimensions = dimensions;
            CategoryID = categoryID;
            IsUsed = false; ;
        }

        public Product(int productID, string name, string description, string brand, decimal price, int stockQuantity, string color, string dimensions, int categoryID, bool isUsed)
        {
            ProductID = productID;
            Name = name;
            Description = description;
            Brand = brand;
            Price = price;
            StockQuantity = stockQuantity;
            Color = color;
            Dimensions = dimensions;
            CategoryID = categoryID;
            IsUsed = false; ;
        }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }
        public int CategoryID { get; set; }
        public bool IsUsed { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
    }
}

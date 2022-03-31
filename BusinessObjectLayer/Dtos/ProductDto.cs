namespace BusinessObjectLayer.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string EanCode { get; set; }

        public int? MinimumQuantity { get; set; }

        public int CategoryId { get; set; }

        public int SubcategoryId { get; set; }

        public float Price { get; set; }

        public string ImagePath { get; set; }

    }
}

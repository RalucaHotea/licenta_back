namespace BusinessObjectLayer.Dtos
{
    public class ProductWarehouseDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductDto Product { get; set; }

        public virtual  WarehouseDto Warehouse { get; set; }
    }
}

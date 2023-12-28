namespace VirtualManager.Shared
{
    public class ProductTax
    {
        public ProductTax()
        {
        }

        public int Id { get; set; }
        public Tax Tax { get; set; }
        public decimal Quantity { get; set; }
        public int TaxId { get; set; }
    }
}
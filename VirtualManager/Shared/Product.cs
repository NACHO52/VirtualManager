namespace VirtualManager.Shared
{
    public class Product
    {
        public Product()
        {
            Id = 0;
            Active = true;
            Resources = new List<ProductResourceItem>();
            Taxes = new List<ProductTax>();
            Name = string.Empty;
            Description = string.Empty;
        }
        public Product(Product obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
            Active = obj.Active;
            Resources = new List<ProductResourceItem>(obj.Resources);
            Taxes = new List<ProductTax>(obj.Taxes);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public IList<ProductResourceItem> Resources { get; set; }
        public IList<ProductTax> Taxes { get; set; }

    }
}
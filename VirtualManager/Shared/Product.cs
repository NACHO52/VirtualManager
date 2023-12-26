namespace VirtualManager.Shared
{
    public class Product
    {
        public Product()
        {
            Resources = new List<ResourceItem>();
            Taxes = new List<Tax>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public IList<ResourceItem> Resources { get; set; }
        public IList<Tax> Taxes { get; set; }

    }
}
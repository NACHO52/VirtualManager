namespace VirtualManager.Shared
{
    public class ProductResourceItem
    {
        public ProductResourceItem()
        {
        }

        public int Id { get; set; }
        public ResourceItem ResourceItem { get; set; }
        public decimal Quantity { get; set; }
        public int ResourceItemId { get; set; }
        public decimal Price 
        { 
            get 
            {
                return ResourceItem != null ? Quantity * ResourceItem.Price : 0;
            } 
        }

    }
}
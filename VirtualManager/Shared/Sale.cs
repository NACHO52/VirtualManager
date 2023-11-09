namespace VirtualManager.Shared
{
    public class Sale
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Client Client { get; set; }
        public SaleStatus Status { get; set; }
    }

    public enum SaleStatus
    {
        None = 0,
        Success = 1,
        Cancelled = 2
    }
}
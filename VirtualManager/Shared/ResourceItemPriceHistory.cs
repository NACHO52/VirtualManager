using System.ComponentModel.DataAnnotations;

namespace VirtualManager.Shared
{
    public class ResourceItemPriceHistory
    {
        public ResourceItemPriceHistory()
        {
            Id = 0;
            Price = 0;
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public string DateTimeStr
        {
            get
            {
                return Date.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
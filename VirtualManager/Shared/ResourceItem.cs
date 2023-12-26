using System.ComponentModel.DataAnnotations;

namespace VirtualManager.Shared
{
    public class ResourceItem
    {
        public ResourceItem()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            MeasureType = ResourceItemMeasureType.Quantity;
            MeasureValue = 0;
            Price = 0;
            PriceHistory = new List<ResourceItemPriceHistory>();
        }
        public ResourceItem(ResourceItem obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
            MeasureType = obj.MeasureType;
            MeasureValue = obj.MeasureValue;
            Price = obj.Price;
            PriceHistory = new List<ResourceItemPriceHistory>(obj.PriceHistory);
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ResourceItemMeasureType MeasureType { get; set; }

        public decimal MeasureValue { get; set; }

        public IList<ResourceItemPriceHistory> PriceHistory { get; set; }
    }

    public enum ResourceItemMeasureType
    {
        Quantity = 1,
        Area = 2
    }
}
namespace VirtualManager.Shared
{
    public class Resource
    {
        public Resource()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            MeasureType = ResourceMeasureType.Quantity;
            MeasureValue = 0;
            Price = 0;
        }
        public Resource(Resource obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
            MeasureType = obj.MeasureType;
            MeasureValue = obj.MeasureValue;
            Price = obj.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ResourceMeasureType MeasureType { get; set; }
        public decimal MeasureValue { get; set; }
    }

    public enum ResourceMeasureType
    {
        Quantity = 1,
        Area = 2
    }
}
using System.Diagnostics;

namespace VirtualManager.Shared
{
    public class Tax
    {
        public Tax()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            Amount = 0;
            Type = TaxType.Percentage;
            AmountHistory = new List<TaxAmountHistory>();
        }
        public Tax(Tax obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Description = obj.Description;
            Amount = obj.Amount;
            Type = obj.Type;
            AmountHistory = new List<TaxAmountHistory>(obj.AmountHistory);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TaxType Type { get; set; }
        public IList<TaxAmountHistory> AmountHistory { get; set; }
    }

    public enum TaxType
    {
        Percentage = 1,
        Constant = 2
    }
}
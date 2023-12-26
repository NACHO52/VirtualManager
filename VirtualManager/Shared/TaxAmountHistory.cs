namespace VirtualManager.Shared
{
    public class TaxAmountHistory
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
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
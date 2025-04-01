namespace ECommerceService.API.Helpers
{
    public class FilterParameters
    {
        public string SearchTerm { get; set; } = string.Empty;
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; } = decimal.MaxValue;
        public bool ValidAmount => MaxAmount > MinAmount;
    }
}

namespace ECommerceService.API.Helpers
{
    public class SortParameters
    {
        public SortingType SortType { get; set; } = SortingType.Descending;
        public string SortMember { get; set; }


        public enum SortingType
        {
            Ascending,
            Descending,
        }
    }
}


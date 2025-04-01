namespace ECommerceService.API.Events
{
    public class UserRegisteredEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string EmailConfirmationToken { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

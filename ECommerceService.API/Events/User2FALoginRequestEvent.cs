namespace ECommerceService.API.Events
{
    public class User2FALoginRequestEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

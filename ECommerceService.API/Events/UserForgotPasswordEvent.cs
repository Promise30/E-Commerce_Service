namespace ECommerceService.API.Events
{
    public class UserForgotPasswordTokenEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public DateTime DatePublished { get; set; }
    }
}

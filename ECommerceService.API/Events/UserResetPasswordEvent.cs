namespace ECommerceService.API.Events
{
    public class UserResetPasswordEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public DateTime DatePublished { get; set; }
    }
}

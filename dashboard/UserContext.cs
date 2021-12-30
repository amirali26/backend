namespace dashboard.context
{
    internal interface IUserContext
    {
        string UserId { get; set; }
        string AccountId { get; set; }
    }

    public class UserContext
    {
        public string UserId { get; set; }
        public string AccountId { get; set; }
    }
}
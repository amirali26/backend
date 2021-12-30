namespace dashboard.Utilities
{
    internal interface IEmailSubmission
    {
        string EmailAddress { get; set; }
    }

    public abstract class EmailSubmission : IEmailSubmission
    {
        public string EmailAddress { get; set; }
    }
}
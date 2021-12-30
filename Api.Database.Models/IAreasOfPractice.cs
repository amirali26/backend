namespace Api.Database.Models
{
    public interface IAreasOfPractice
    {
        int Id { get; set; }
        string ExternalId { get; set; }
        string Name { get; set; }
    }
}
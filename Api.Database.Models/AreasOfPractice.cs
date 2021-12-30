using HotChocolate;

namespace Api.Database.Models
{
    public class AreasOfPractice : IAreasOfPractice
    {
        [GraphQLIgnore] public int Id { get; set; }

        [GraphQLName("id")] public string ExternalId { get; set; }

        public string Name { get; set; }
    }
}
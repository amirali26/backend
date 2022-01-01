using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace Api.Database.Models
{
    public class AccountUserInvitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLIgnore]
        public int Id { get; set; }
        [GraphQLName("id")] public string ExternalId { get; set; }
        public string UserEmail { get; set; }
        public User ReferUser { get; set; }
        public Account Account { get; set; }
    }
}
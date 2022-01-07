using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace Api.Database.Models
{
    public enum AccountUserInvitationStatus
    {
        PENDING = 0,
        REJECTED = 1,
        ACCEPTED = 2,
    }
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
        public DateTime CreatedAt { get; set; }
        public AccountUserInvitationStatus Status { get; set; }
    }
}
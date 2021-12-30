using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace Api.Database.Models
{
    // Some comment
    public class User : IUser
    {
        public ICollection<Account> CreatedAccounts { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLIgnore]
        public int Id { get; set; }

        [GraphQLName("id")] public string ExternalId { get; set; }

        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
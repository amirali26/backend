using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

// Learn about normal forms
namespace Api.Database.Models
{
    public class Account : IAccount
    {
        public ICollection<AreasOfPractice> AreasOfPractice { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLIgnore]
        public int Id { get; set; }

        [GraphQLName("id")] public string ExternalId { get; set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        [InverseProperty("CreatedAccounts")] public User CreatedBy { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
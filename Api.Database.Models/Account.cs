using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

// Learn about normal forms
namespace Api.Database.Models
{
    public enum AccountType
    {
        LONDON_LARGE_COMMERCIAL = 0,
        LONDON_AMERICAN_FIRMS = 1,
        LONDON_MID_SIZED_COMMERCIAL = 2,
        LONDON_SMALLER_COMMERCIAL = 3,
        REGIONAL_FIRMS = 4,
        GENERAL_PRACTICE_SMALL_FIRMS = 5,
        NATIONAL_MULTISITE_FIRMS = 6,
        NICHE_FIRMS = 7,
    }
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
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public AccountType Size { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime CreatedAt { get; set; }
        [InverseProperty("CreatedAccounts")] 
        public User CreatedBy { get; set; }
        public ICollection<User> Users { get; set; }
    }
}

/*
    London / Large Commercial
    London / American Firms
    London / Mid Sized Commercial
    London / Smaller Commercial
    Regional Firms
    General Practice / Small Firms
    National / Multi-Site Firms
    Niche Firms
*/
using System;
using System.Collections.Generic;

namespace Api.Database.Models
{
    public interface IUser
    {
        int Id { get; set; }
        string ExternalId { get; set; }
        string Name { get; set; }
        string DateOfBirth { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        DateTime CreatedAt { get; set; }
        ICollection<Account> Accounts { get; set; }
    }
}
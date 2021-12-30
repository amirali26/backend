using System;
using System.Collections.Generic;

namespace Api.Database.Models
{
    public interface IAccount
    {
        int Id { get; set; }
        string ExternalId { get; set; }
        string Name { get; set; }
        ICollection<User> Users { get; set; }
        User CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
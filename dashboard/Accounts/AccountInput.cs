using System;
using System.Collections.Generic;
using Api.Database.Models;

namespace dashboard.Accounts
{
    public class AccountInput
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AccountType Type { get; set; }
        public DateTime  RegisteredDate { get; set; }
        public List<string> AreasOfPracticeId { get; set; }
        public List<string> Users { get; set; }
    }
}
using System.Collections.Generic;

namespace dashboard.Users
{
    public class UserInput
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<int> AccountIds { get; set; }
    }
}
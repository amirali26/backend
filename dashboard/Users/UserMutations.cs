using System;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using HotChocolate;
using HotChocolate.Types;

namespace dashboard.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        public async Task<User> AddUserAsync([Service] DashboardContext context, UserInput userInput)
        {
            var user = new User
            {
                ExternalId = Guid.NewGuid().ToString(),
                Name = userInput.Name,
                CreatedAt = DateTime.UtcNow,
                DateOfBirth = userInput.DateOfBirth,
                PhoneNumber = userInput.PhoneNumber,
                Email = userInput.Email
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
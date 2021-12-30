using System;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using HotChocolate;
using HotChocolate.Types;

namespace dashboard.Clients
{
    [ExtendObjectType(Name = "Mutation")]
    public class ClientMutations
    {
        public async Task<Client> AddUserAsync([Service] DashboardContext context, ClientInput userInput)
        {
            var user = new Client
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.AccountUserInvitations;
using dashboard.context;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Accounts
{
    [ExtendObjectType(Name = "Mutation")]
    public class AccountMutations
    {
        public async Task<Account> AddAccount([GlobalState("UserContext")] UserContext userContext,
            [Service] IAccountUserInvitationService accountUserInvitationService,
            [Service] DashboardContext context, AccountInput accountInput)
        {
            var user = await context.Users.Select(u => u).Where(u => u.ExternalId == userContext.UserId)
                .FirstOrDefaultAsync();
            var areasOfPractices = await context.AreasOfPractice.Select(aop => aop)
                .Where(aop => accountInput.AreasOfPracticeId.Contains(aop.ExternalId)).ToListAsync();

            var usersList = new List<User>
            {
                user
            };

            var account = new Account
            {
                Name = accountInput.Name,
                Users = usersList,
                CreatedAt = DateTime.Now,
                CreatedBy = user,
                ExternalId = Guid.NewGuid().ToString(),
                AreasOfPractice = areasOfPractices,
                Email = accountInput.Email,
                PhoneNumber = accountInput.PhoneNumber,
                Website = accountInput.Website,
                Size = accountInput.Type,
                RegisteredDate = accountInput.RegisteredDate,
            };

            accountInput.Users.ForEach(async (u) =>
            {
                await accountUserInvitationService.AddAccountUserInvitationService(user, u, account);
            });
            
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();

            return account;
        }
    }
}
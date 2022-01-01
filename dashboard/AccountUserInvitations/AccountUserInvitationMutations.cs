using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace dashboard.AccountUserInvitations
{
    [ExtendObjectType(Name = "Mutation")]
    public class AccountUserInvitationMutations
    {
        public async Task<AccountUserInvitation>AddAccountUserInvitation([GlobalState("UserContext")] UserContext userContext,
            [Service] IAccountUserInvitationService accountUserInvitationService,
            [Service] DashboardContext context,
            AccountUserInvitationInput accountUserInvitationInput)
        {
            var user = await context.Users.Select(u => u).Where(u => u.ExternalId == userContext.UserId)
                .FirstOrDefaultAsync();
            var account = await context.Accounts.Where(a => a.ExternalId == userContext.AccountId)
                .FirstAsync();
            return await accountUserInvitationService.AddAccountUserInvitationService(
                user,
                accountUserInvitationInput.UserEmail,
                account
            );
        }
    }
}
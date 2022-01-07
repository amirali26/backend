using System.Collections.Generic;
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
        public async Task<AccountUserInvitation> AddAccountUserInvitation(
            [GlobalState("UserContext")] UserContext userContext,
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

        public async Task<AccountUserInvitation> AcceptOrRejectAccountUserInvitation(
            [GlobalState("UserContext")] UserContext userContext,
            [Service] DashboardContext context,
            string accountUserInvitationId,
            int accountUserInvitationStatus
        )
        {
            var aui = await context.AccountUserInvitations
                .Include(au => au.Account
                ).FirstAsync(au => au.ExternalId == accountUserInvitationId);

            aui.Status = (AccountUserInvitationStatus)accountUserInvitationStatus;
                
            if ((AccountUserInvitationStatus)accountUserInvitationStatus == AccountUserInvitationStatus.ACCEPTED)
            {
                var user = await context.Users.Select(u => u).Where(u => u.ExternalId == userContext.UserId)
                    .FirstOrDefaultAsync();
                if (user.Accounts == null)
                {
                    user.Accounts = new List<Account>()
                    {
                        aui.Account
                    };
                }
                else
                {
                    user.Accounts.Add(aui.Account);
                }
                context.Users.Update(user);
            }


            await context.SaveChangesAsync();

            return aui;
        }
    }
}
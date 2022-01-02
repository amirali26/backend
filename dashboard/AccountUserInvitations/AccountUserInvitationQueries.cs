using System.Linq;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace dashboard.AccountUserInvitations
{
    [ExtendObjectType(Name = "Query")]
    public class AccountUserInvitationQueries
    {
        [UseProjection]
        public IQueryable<AccountUserInvitation> GetAccountUserInvitations([Service] DashboardContext context,
            [GlobalState("UserContext")] UserContext userContext)
        {
            var userEmail = context.Users.Where(u => u.ExternalId == userContext.UserId).First().Email;
            return context.AccountUserInvitations.Where(a => a.UserEmail == userEmail);
        }
    }
}
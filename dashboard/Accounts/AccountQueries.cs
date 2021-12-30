using System.Linq;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace dashboard.Accounts
{
    [ExtendObjectType(Name = "Query")]
    public class AccountQueries
    {
        [UseProjection]
        public IQueryable<Account> GetUserAccounts([GlobalState("UserContext")] UserContext userContext,
            [Service] DashboardContext context)
        {
            return context.Accounts.Where(a => a.Users.Any(u => u.ExternalId == userContext.UserId));
        }

        [UseProjection]
        public IQueryable<Account> GetUserAccount([GlobalState("UserContext")] UserContext userContext,
            [Service] DashboardContext context, string accountId)
        {
            return context.Accounts.Where(a => a.ExternalId == accountId
                                               && a.Users.Any(u => u.ExternalId == userContext.UserId));
        }
    }
}
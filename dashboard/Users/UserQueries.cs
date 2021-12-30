using System.Linq;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace dashboard.Users
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [UseProjection]
        public IQueryable<User> GetUser(
            [GlobalState("UserContext")] UserContext userContext, [Service] DashboardContext context)
        {
            return context.Users.Where(u => u.ExternalId == userContext.UserId);
        }
    }
}
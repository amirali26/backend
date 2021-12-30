using System.Linq;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace dashboard.Requests
{
    [ExtendObjectType(Name = "Query")]
    public class RequestQueries
    {
        [UseProjection]
        public IQueryable<Request> GetRequests([Service] DashboardContext context,
            [GlobalState("UserContext")] UserContext userContext)
        {
            return context.Requests.Where(r => r.Enquiries.All(e => e.Account.ExternalId != userContext.AccountId));
        }
    }
}
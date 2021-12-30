using System.Linq;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace dashboard.Enquiries
{
    [ExtendObjectType(Name = "Query")]
    public class EnquiryQueries
    {
        [UseProjection]
        public IQueryable<Enquiry> GetEnquiries([Service] DashboardContext context,
            [GlobalState("UserContext")] UserContext userContext)
        {
            return context.Enquiries.Where(e => e.Account.ExternalId == userContext.AccountId);
        }
    }
}
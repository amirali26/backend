using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using dashboard.context;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;

namespace dashboard
{
    public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
    {
        public override ValueTask OnCreateAsync(HttpContext context,
            IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? accountId = context.Request.Headers["accountId"];

            requestBuilder.SetProperty("UserContext", new UserContext
            {
                // UserId = "d28ae761-0f3c-4a43-8fc9-136624045e58",
                // AccountId = 8ae761-0f3c-4a43-8fc9-136624045e58",
                UserId = userId,
                AccountId = accountId
            });

            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);
        }
    }
}
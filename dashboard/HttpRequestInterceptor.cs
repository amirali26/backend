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
                // UserId = "bfa1cfaa-93a4-473e-b123-7942305c9c06",
                // AccountId = "197d5871-2806-459d-a637-7ad938c6bcbc",
                UserId = userId,
                AccountId = accountId
            });

            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);
        }
    }
}
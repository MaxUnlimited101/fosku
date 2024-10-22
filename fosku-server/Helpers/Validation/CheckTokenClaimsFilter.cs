using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using fosku_server.Services.Auth;

namespace SharperExpenser.Helpers.Validation;

public class CheckTokenClaimsFilter : Attribute, IAsyncResourceFilter
{
    private IAuthService? _authService = null;

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        _authService = context.HttpContext.RequestServices.GetService<IAuthService>();
        var UserId = _authService.ValidateToken(context.HttpContext.Request.Cookies["access_token"]);
        if (UserId == string.Empty)
        {
            context.Result = new UnauthorizedResult();
        }
        IDictionary<string, string?> query = context.HttpContext.Request.Query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        query["userId"] = UserId;

        var newQueryString = QueryHelpers.AddQueryString(string.Empty, query);
        context.HttpContext.Request.QueryString = new QueryString(newQueryString);

        await next();
    }
}
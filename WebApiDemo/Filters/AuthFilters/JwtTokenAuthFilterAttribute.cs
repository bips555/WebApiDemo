using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Attributes;
using WebApiDemo.Authority;

namespace WebApiDemo.Filters.AuthFilters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
               context.Result = new UnauthorizedResult();
                return;
            }
            var config =  context.HttpContext.RequestServices.GetService<IConfiguration>();
          var claims = Authenticator.VerifyToken(token, config.GetValue<string>("Secretkey"));
          
            if(claims == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var requiredClaims =  context.ActionDescriptor.EndpointMetadata
     .OfType<RequiredClaimAttribute>()
     .ToList();

                if (requiredClaims != null && !requiredClaims.All(rc => claims.Any(c =>
                        string.Equals(c.Type, rc.ClaimType, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(c.Value, rc.ClaimValue, StringComparison.OrdinalIgnoreCase))))
                {
                    context.Result = new StatusCodeResult(403);
                }

            }
        }
    }
}

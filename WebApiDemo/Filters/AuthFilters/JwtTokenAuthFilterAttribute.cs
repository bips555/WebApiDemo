﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
            if (!Authenticator.VerifyToken(token, config.GetValue<string>("Secretkey")))
            {
               context.Result = new UnauthorizedResult();
            }
        }
    }
}

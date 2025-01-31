﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Models;

namespace WebApiDemo.Filters.ActionFilters
{
    public class Shirt_UpdateShirtFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            var shirt = context.ActionArguments["shirt"] as Shirt;
            if (id.HasValue && shirt != null && id != shirt.ShirtId)
            {
                context.ModelState.AddModelError("ShirtId", "ShirtId is not the same as Id");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }

        }
    }
}


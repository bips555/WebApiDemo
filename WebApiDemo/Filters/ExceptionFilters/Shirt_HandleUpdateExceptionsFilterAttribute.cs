﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Data;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionsFilterAttribute:ExceptionFilterAttribute
    {
        private readonly ApplicationDbContext _context;
        public Shirt_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext context)
        {
            _context = context;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirtId = context.RouteData.Values["id"] as string;
            
            if (int.TryParse(strShirtId,out int shirtId))
            {
               if(_context.Shirts.FirstOrDefault(x=>x.ShirtId == shirtId)==null)
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt does not existanymore");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}

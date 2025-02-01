using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using System.Reflection;
using WebApiDemo.Data;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Filters.ActionFilters
{
    public class Shirt_CreateShirtFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;
        public Shirt_CreateShirtFilterAttribute(ApplicationDbContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null)
            {
                context.ModelState.AddModelError("Shirt", "Shirt is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingShirt = _context.Shirts.FirstOrDefault(x => !String.IsNullOrEmpty(shirt.Brand) &&
            !String.IsNullOrEmpty(x.Brand) &&
            x.Brand.ToLower() == shirt.Brand.ToLower() &&
            !String.IsNullOrEmpty(shirt.Color) &&
                !String.IsNullOrEmpty(x.Color) &&
            x.Color.ToLower() == shirt.Color.ToLower() &&
            !String.IsNullOrEmpty(shirt.Gender) &&
                !String.IsNullOrEmpty(x.Gender) &&
            x.Gender.ToLower() == shirt.Gender.ToLower() &&
                x.Size.HasValue &&
            shirt.Size.HasValue &&
            x.Size.Value == shirt.Size.Value);
                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("Shirt", "Shirt already exists");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }

        }
    }
}

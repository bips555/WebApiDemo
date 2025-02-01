using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Data;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Filters
{
    public class Shirt_ValidateShirtIdFilterAttribute:ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;
        public Shirt_ValidateShirtIdFilterAttribute(ApplicationDbContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           base.OnActionExecuting(context);
            var shirtId = context.ActionArguments["id"] as int?;
            if(shirtId.HasValue)
            {
                if (shirtId.Value <= 0)
                {
                    context.ModelState.AddModelError("ShirtId", "ShirtId is not valid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    var shirt = _context.Shirts.Find(shirtId.Value);
                   if(shirt == null)
                    {
                        context.ModelState.AddModelError("ShirtId", "ShirtId does not exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                    else
                    {
                        context.HttpContext.Items["shirt"] = shirt;
                    }
                }
            }
            
        }
    }
}

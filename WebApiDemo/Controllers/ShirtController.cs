
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Filters;
using WebApiDemo.Filters.ActionFilters;
using WebApiDemo.Filters.ExceptionFilters;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShirtController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShirtController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
       
        public IActionResult GetShirts()
        {
            return Ok(_context.Shirts.ToList());
        }
        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }
        [HttpPost]
        [TypeFilter(typeof(Shirt_CreateShirtFilterAttribute))]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {

            _context.Add(shirt);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetShirtById), new { Id = shirt.ShirtId },shirt);
        }
        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_UpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
           
                ShirtRepository.UpdateShirt(shirt);
                return NoContent();
          
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult DeleteShirt(int id)
        {
            var existingShirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(existingShirt);

        }

    }
}

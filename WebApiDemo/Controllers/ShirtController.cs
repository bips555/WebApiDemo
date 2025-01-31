
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShirtController : ControllerBase
    {
       
        [HttpGet]
       
        public IActionResult GetShirts()
        {
            return Ok("Reading All the Shirts");
        }
        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }
        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            return Ok("Creating Shirts");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id)
        {
            return Ok($"Updating Shirt with Id {id}");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Deleting Shirt with Id {id}");
        }

    }
}

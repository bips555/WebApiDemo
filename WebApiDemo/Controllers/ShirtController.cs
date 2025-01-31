
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetShirtById(int id)
        {
            if(id < 0)
            {
                return BadRequest();
            }
            var shirt = ShirtRepository.GetShirtById(id);
            if (!ShirtRepository.ShirtExists(id))
            {
                return NotFound();
            }
            return Ok(shirt);
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


using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShirtController : ControllerBase
    {
        private List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt{ShirtId=1,Size=12,Brand="A",Price=12,Gender="Men",Color="Blue"},
            new Shirt{ShirtId=2,Size=11,Brand="B",Price=22,Gender="Women",Color="Red"},
            new Shirt{ShirtId=3,Size=14,Brand="C",Price=32,Gender="Women",Color="Green"},
            new Shirt{ShirtId=4,Size=13,Brand="D",Price=42,Gender="Men",Color="White"},
            new Shirt{ShirtId=5,Size=15,Brand="E",Price=52,Gender="Women",Color="Black"},

        };
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
            var shirt = shirts.FirstOrDefault(x => x.ShirtId == id);
            if(shirt == null)
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

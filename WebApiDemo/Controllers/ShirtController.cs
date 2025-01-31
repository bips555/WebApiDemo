﻿
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShirtController : ControllerBase
    {
        [HttpGet]
       
        public string GetShirts()
        {
            return "Reading All the Shirts";
        }
        [HttpGet("{id}")]
        public string GetShirtById(int id, [FromHeader]string color)
        {
            return $"Reading Shirt with Id {id} and color: {color}";
        }
        [HttpPost]
        public string CreateShirt([FromBody] Shirt shirt)
        {
            return "Creating shirt";
        }
        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating Shirt with Id {id}";
        }
        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting Shirt with Id {id}";
        }

    }
}

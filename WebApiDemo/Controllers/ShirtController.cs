
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [ApiController]
    public class ShirtController : ControllerBase
    {
        public string GetShirts()
        {
            return "Reading All the Shirts";
        }
        public string GetShirtById(int id)
        {
            return $"Reading Shirt with Id {id}";
        }
        public string CreateShirt()
        {
            return "Creating Shirt";
        }
        public string UpdateShirt(int id)
        {
            return $"Updating Shirt with Id {id}";
        }
        public string DeleteShirt(int id)
        {
            return $"Deleting Shirt with Id {id}";
        }

    }
}

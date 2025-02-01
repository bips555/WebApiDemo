using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class ShirtController : Controller
    {
        public IActionResult Index()
        {
            
            return View(ShirtRepository.GetShirts().ToList());
        }
    }
}

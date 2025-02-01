using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IWebApiExecutor _webApiExecutor;
        public ShirtController(IWebApiExecutor webApiExecutor)
        {
            _webApiExecutor = webApiExecutor;
        }
        public async Task< IActionResult> Index()
        {
            
            return View(await _webApiExecutor.InvokeGet<List<Shirt>>("shirt"));
        }
    }
}

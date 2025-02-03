using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Repositories;
using WebApp.Models.Validations;

namespace WebApp.Controllers
{
    public class ShirtController : Controller
    {
        private readonly IWebApiExecutor _webApiExecutor;
        public ShirtController(IWebApiExecutor webApiExecutor)
        {
            _webApiExecutor = webApiExecutor;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _webApiExecutor.InvokeGet<List<Shirt>>("shirt"));
        }
        public IActionResult CreateShirt()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _webApiExecutor.InvokePost<Shirt>("shirt", shirt);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (WebApiException ex)
                {
                    HandleWebApiExeption(ex);
                }
               
            }
            return View(shirt);
        }
        public async Task<IActionResult> UpdateShirt(int shirtId)
        {
            try
            {
                var shirt = await _webApiExecutor.InvokeGet<Shirt>($"shirt/{shirtId}");
                if (shirt != null)
                {
                    return View(shirt);
                }
            }
            catch (WebApiException ex)
            {
                HandleWebApiExeption(ex);
                return View();
            }
           

            return NotFound(); 
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _webApiExecutor.InvokePut<Shirt>($"shirt/{shirt.ShirtId}", shirt);
                    return RedirectToAction(nameof(Index));
                }catch(WebApiException ex)
                {
                    HandleWebApiExeption(ex);
                   
                }
            }
            return View(shirt);
        }
        public async Task<IActionResult> DeleteShirt(int shirtId)
        {
                await _webApiExecutor.InvokeDelete<Shirt>($"shirt/{shirtId}");
                return RedirectToAction(nameof(Index));
           
        }
        private void HandleWebApiExeption(WebApiException ex)
        {
            if (ex.ErrorResponse != null && ex.ErrorResponse.Errors != null && ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join(";", error.Value));
                }
            }
        }
    }
}

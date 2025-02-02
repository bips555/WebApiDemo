﻿using Microsoft.AspNetCore.Mvc;
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
                await _webApiExecutor.InvokePost<Shirt>("shirt", shirt);
                return RedirectToAction(nameof(Index));
            }
            return View(shirt);
        }
        public async Task<IActionResult> UpdateShirt(int shirtId)
        {
            var shirt = await _webApiExecutor.InvokeGet<Shirt>($"shirt/{shirtId}");
            if (shirt != null)
            {
                return View(shirt);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

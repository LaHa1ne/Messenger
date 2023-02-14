﻿using messenger2.DataLayer.DTO;
using messenger2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace messenger2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        //[IgnoreAntiforgeryToken]
        public JsonResult MyJson([FromBody] UserIdDTO data)
        {
            var id = data.UserId;
            return Json(new {el="abc"});
        }

    }
}
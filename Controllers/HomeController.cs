using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackathon.Models;

namespace hackathon.Controllers
{
    [Route("api/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Contact(string id)
        {
            Sicknens sicknens = new Sicknens() { response =id};
            return Json(sicknens);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

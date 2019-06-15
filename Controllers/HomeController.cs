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
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                host = databaseUri.Host,
                port = databaseUri.Port,
                username = userInfo[0],
                password = userInfo[1],
                database = databaseUri.LocalPath.TrimStart('/')
            };
            return Json(builder);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

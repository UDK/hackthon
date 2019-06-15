using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackathon.Models;
using Npgsql;

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
        public JsonResult Contact(string id)
        {
            Sicknens result = new Sicknens();
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var conn = new NpgsqlConnection(databaseUrl);
            conn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM patient", conn);
                var dr = command.ExecuteReader();
                dr.Read();
                result.id = (uint)dr[0];
                result.name = (string)dr[1];
                result.surname = (string)dr[0];
                return Json(result);
            }
            catch(InvalidCastException e)
            {
                return e.ToString();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

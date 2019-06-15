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
        public IActionResult Contact(string id)
        {
            Sicknens result = new Sicknens();
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };
            var conn = new NpgsqlConnection(databaseUrl);
            //conn.Open();
            //using (var cmd = new NpgsqlCommand())
            //{
            //    cmd.Connection = conn;
            //    cmd.CommandText = "SELECT * FROM patient";
            //    var dr = cmd.ExecuteReader();
            //    dr.Read();
            //    result.id = (uint)dr[0];
            //    result.name = (string)dr[1];
            //    result.surname = (string)dr[0];
            //}
            return Json(userInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

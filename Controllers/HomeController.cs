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
            var qq = Interface("asd",1,"asdas");
            return qq;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        private JsonResult Interface(string table,int id,string table_compare)
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
            try
            {
                string joinSQL;
                //if(table_compare == ""|| table_compare == null)
                //{
                //    joinSQL = " JOIN "+table_compare+" on "+ table_compare+'.'++id +" = "+table+;
                //}
                var conn = new NpgsqlConnection(builder.ToString());
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM sickness");
                var dr = command.ExecuteReader();
                dr.Read();
                result.id = (int)dr[0];
                result.name = (string)dr[1];
                result.surname = (string)dr[2];
                dr.Close();
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}

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
            return Interface("sickness", Convert.ToInt32(id), "patient");
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
                var conn = new NpgsqlConnection(builder.ToString());
                conn.Open(); 
                //sql запрос нормально бы оформить
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM "+table+ " join "+table_compare+" on patient.id = sickness.idPatient where patient.id="+id, conn);
                var dr = command.ExecuteReader();
                dr.Read();
                result.id = (int)dr[0];
                result.idPre= (int)dr[1];
                result.idPat = (int)dr[2];
                result.idDoc = (int)dr[3];
                result.text = (string)dr[4];
                result.comment = (string)dr[5];
                conn.Close();
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}

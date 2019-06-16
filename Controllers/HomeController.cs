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

        [HttpGet]
        public JsonResult Contact(string id)
        {
            return Interface("sickness", Convert.ToInt32(id), "prescripion");
        }
        
        private JsonResult Interface(string table,int id,string table_compare)
        {
            Sicknens result = new Sicknens();
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            Dictionary<string, string> mass = new Dictionary<string, string>();
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM "+table+ " join "+table_compare+" on "+table_compare+".id = sickness.id"+ FirstUpper(table_compare)+ " where "+table_compare+".id="+id, conn);
                var dr = command.ExecuteReader();
                dr.Read();
                for(int i=0;i<dr.VisibleFieldCount;i++)
                {
                    mass.Add(i.ToString(),dr[i].ToString());
                }
                //result.id = (int)dr[0];
                //result.idPre= (int)dr[1];
                //result.idPat = (int)dr[2];
                //result.idDoc = (int)dr[3];
                //result.text = (string)dr[4];
                //result.comment = (string)dr[5];
                conn.Close();
                return Json(mass);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
        private string FirstUpper(string str)
        {
            return str[0].ToString().ToUpper() + str.Substring(1); 
        }
    }
}

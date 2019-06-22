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
        public JsonResult Contact(string id,string table)
        {
            if (table == "pat")
            {
                return Interface("sickness", Convert.ToInt32(id), "patient");
            }
            else if (table == "pre")
            {
                return Interface("sickness", Convert.ToInt32(id), "prescripion","idmediacament");
            }
            else if (table == "drug")
            {
                return Interface("prescripion", Convert.ToInt32(id), "medicament", "medicament.id");
            }
            else if (table =="doctor")
            {
                return Interface("sickness", Convert.ToInt32(id), "doctor", "doctor.name, doctor.surname, doctor.post");
            }
            else
            {
                return null;
            }
        }
        
        private JsonResult Interface(string table,int id,string table_compare,string select="*")
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT "+select+" FROM "+table+ " join "+table_compare+" on "+table_compare+".id = "+table+".id"+ FirstUpper(table_compare)+ " where "+table_compare+".id="+id, conn);
                return Json(command);
                var dr = command.ExecuteReader();
                dr.Read();
                for(int i=0;i<dr.VisibleFieldCount;i++)
                {
                    mass.Add(i.ToString(),dr[i].ToString());
                }
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

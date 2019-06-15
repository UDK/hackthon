using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon.Models
{
    public class Sicknens
    {
        public string response { get; set; }
    }
    public class NpgsqlConnectionStringBuilder
    {
        public string host { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }
    }
}

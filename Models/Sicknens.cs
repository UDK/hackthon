using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon.Models
{
    public class Sicknens
    {
        public string id { get; set; }
        public int idPre { get; set; }
        public int idPat { get; set; }
        public int idDoc { get; set; }
        public string text { get; set; }
        public string comment { get; set; }
    }
}

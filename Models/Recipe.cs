using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon.Models
{
    public class Recipe
    {
        public Int64 polic { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public List<Drug> drugs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackathon.Models
{
    public class Drug
    {
        public int img { get; set; }
        public string name { get; set; }
        public string substances { get; set; }
        public int price { get; set; }
        public int periodBeginD { get; set; }
        public int periodBeginM { get; set; }
        public int periodBeginY { get; set; }
        public int periodEndD { get; set; }
        public int periodEndM { get; set; }
        public int periodEndY { get; set; }
        public bool warning { get; set; }
        public string doza { get; set; }
        public string conditions { get; set; }
        public int id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sr6.Models
{
    public class Project
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<Scenario> Scenarios { get; set; }
    }
}
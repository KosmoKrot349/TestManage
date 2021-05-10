using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sr6.Models
{
    public class StatViewModel
    {
        public Project selectProject {get;set;}
        public List<Project> projects { get; set; }
        public int countOfTests { get; set; }
        public int countWithSuccessCompleet { get; set; }
        public int countWithUnSuccessCompleet { get; set; }
        public int countOfErrors { get; set; }
        public int countOfFixedErreros { get; set; }
        public int countOfWaitingToFix { get; set; }
    }
}
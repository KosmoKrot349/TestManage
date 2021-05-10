using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sr6.Models
{
    public class DBContext:DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Action> Actions { get; set; }
    }
}
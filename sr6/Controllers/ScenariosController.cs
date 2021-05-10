using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sr6.Models;
using System.Data.Entity;

namespace sr6.Controllers
{
    public class ScenariosController : Controller
    {

        DBContext context = new DBContext();
        // GET: Scenarios
        public ActionResult ScenariosList(string project)
        {
            if (String.IsNullOrEmpty(project)) return RedirectToAction("Index","Home");
            ScenariosListViewModel scenariosListVM = new ScenariosListViewModel();
            scenariosListVM.Scenarios = context.Scenarios.Include(s => s.project).Where(p => p.project.title == project).ToList();
         scenariosListVM.project = project;
                return View(scenariosListVM) ;
        }
        [HttpGet]
        public ActionResult AddScenario(string project)
        {
            if (String.IsNullOrEmpty(project)) return RedirectToAction("Index","Home");
            Scenario scenario = new Scenario();
            scenario.Projectid = context.Projects.Where(p => p.title == project).FirstOrDefault().id;
            return View(scenario);
        }
        [HttpPost]
        public ActionResult AddScenario(string title,string result,DateTime? dateOfExecution,string typeOfError,DateTime? dateOfBugFix, string nameOfFixer, int? ProjectId)
        {
            Scenario scenario = new Scenario();
            scenario.title = title;
            scenario.result = result;
            scenario.isFixed = false;
           scenario.dateOfExecution = Convert.ToDateTime(dateOfExecution);
           scenario.typeOfError = typeOfError;
            scenario.dateOfBugFix = Convert.ToDateTime(dateOfBugFix);
            scenario.nameOfFixer = nameOfFixer;
            scenario.Projectid = Convert.ToInt32(ProjectId);
            context.Scenarios.Add(scenario);
            context.SaveChanges();
            return RedirectToAction("ScenariosList","Scenarios",new { project = context.Projects.Where(p => p.id == ProjectId).FirstOrDefault().title });
        }
        public ActionResult DeleteScenario(int id, string project) {

            context.Scenarios.Remove(context.Scenarios.Include(s=>s.Actions).Where(s=>s.id==id).FirstOrDefault());
            context.SaveChanges();
            return RedirectToAction("ScenariosList", "Scenarios", new { project = project }); 
        }
        public ActionResult FixBug(int? id,string project)
        {
            if (id == null||String.IsNullOrEmpty(project)) { return RedirectToAction("Index", "Home"); }
            Scenario sc = context.Scenarios.Where(s => s.id == id).FirstOrDefault();
            if (sc.isFixed == false) sc.isFixed = true;
            else sc.isFixed = false;
            context.SaveChanges();
            return RedirectToAction("ScenariosList", "Scenarios", new { project = project });

        }
        [HttpGet]
        public ActionResult ChangeScenario(int? id, string project)
        {
            if (id == null || String.IsNullOrEmpty(project)) { return RedirectToAction("Index", "Home"); }
            Scenario sc = context.Scenarios.Where(s => s.id == id).FirstOrDefault();
            return View(sc);
        }

        [HttpPost]
        public ActionResult ChangeScenario(string title, string result, DateTime? dateOfExecution, string typeOfError, DateTime? dateOfBugFix, string nameOfFixer, int? ProjectId,int id)
        {
            Scenario scenario = context.Scenarios.Where(s => s.id == id).FirstOrDefault();
            scenario.title = title;
            scenario.result = result;
            scenario.isFixed = false;
            scenario.dateOfExecution = Convert.ToDateTime(dateOfExecution);
            scenario.typeOfError = typeOfError;
            scenario.dateOfBugFix = Convert.ToDateTime(dateOfBugFix);
            scenario.nameOfFixer = nameOfFixer;
            scenario.Projectid = Convert.ToInt32(ProjectId);
            context.SaveChanges();
            return RedirectToAction("ScenariosList", "Scenarios", new { project = context.Projects.Where(p => p.id == ProjectId).FirstOrDefault().title });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sr6.Models;

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
        public ActionResult AddScenario([Bind(Include = "title,result,dateOfExecution,dateOfBugFix,nameOfFixer,typeOfError,ProjectId")]Scenario scenario)
        {
            scenario.isFixed = false;
            if (ModelState.IsValid)
            {

                context.Scenarios.Add(scenario);
                context.SaveChanges();
                return RedirectToAction("ScenariosList", "Scenarios", new { project = context.Projects.Where(p => p.id == scenario.Projectid).FirstOrDefault().title });
            }
            return View(scenario);
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
            Scenario scenario = context.Scenarios.Where(s => s.id == id).FirstOrDefault();
            return View(scenario);
        }

        [HttpPost]
        public ActionResult ChangeScenario([Bind(Include = "id,title,result,dateOfExecution,dateOfBugFix,nameOfFixer,typeOfError,ProjectId")] Scenario scenario)
        {

            if (ModelState.IsValid)
            {
                context.Entry(scenario).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ScenariosList", "Scenarios", new { project = context.Projects.Where(p => p.id == scenario.Projectid).FirstOrDefault().title });
            }
            return View(scenario);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sr6.Models;
using System.Data.Entity;

namespace sr6.Controllers
{
    public class ActionsController : Controller
    {
        DBContext context = new DBContext();
        public ActionResult ActionList(int? id)
        {
            if (id == null) { return RedirectToAction("Index", "Home"); }
            ActionListViewModel ALWM = new ActionListViewModel();
            ALWM.Actions = context.Actions.Include(a => a.scenario).Where(a => a.Scenarioid == id).OrderBy(a => a.number).ToList();
            ALWM.id = Convert.ToInt32(id);

            return View(ALWM);
        }
        [HttpGet]
        public ActionResult AddAction(int? id)
        {
            if (id == null) { return RedirectToAction("Index", "Home"); }
            Models.Action act = new Models.Action();
            act.number = 1;
            if (context.Actions.Where(a => a.Scenarioid == id).Count() > 0)
                act.number = (context.Actions.Where(a => a.Scenarioid == id).OrderByDescending(a => a.number).First().number) + 1;
            act.Scenarioid = Convert.ToInt32(id);
            return View(act);
        }
        [HttpPost]

        public ActionResult AddAction([Bind(Include = "number,action,Scenarioid")] Models.Action act)
        {
            if (ModelState.IsValid)
            {
                context.Actions.Add(act);
                context.SaveChanges();
                return RedirectToAction("ActionList", "Actions", new { id = act.Scenarioid });
            }
            return View(act);
        }
        public ActionResult DeleteAction(int? ActId, int? id)
        {
            if (ActId == null || id == null) { return RedirectToAction("Index", "Home"); }
            Models.Action act = context.Actions.Where(a => a.id == ActId).FirstOrDefault();
            context.Actions.Remove(act);
            context.SaveChanges();
            return RedirectToAction("ActionList", "Actions", new { id = id });

        }
        [HttpGet]
        public ActionResult ChangeAction(int? ActId, int? id)
        {
            if (ActId == null || id == null) { return RedirectToAction("Index", "Home"); }
            Models.Action act = context.Actions.Where(a => a.id == (int)ActId).FirstOrDefault();
            return View(act);
        }
        [HttpPost]
        public ActionResult ChangeAction([Bind(Include = "number,action,Scenarioid")] Models.Action act,int Aid)
        {
            act.id = Aid;
            if (ModelState.IsValid)
            {
                context.Entry(act).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ActionList", "Actions", new { id = act.Scenarioid });
            }return View(act);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sr6.Models;
using System.Data.Entity;
using ClosedXML.Excel;
using System.IO;

namespace sr6.Controllers
{
    public class HomeController : Controller
    {
        DBContext context = new DBContext();
        public ActionResult Index()
        {
            return View(context.Projects.ToList());
        }

        public ActionResult About(string project)
        {
            StatViewModel SVM = new StatViewModel();

            if (String.IsNullOrEmpty(project))
            {
                if (context.Projects.Count() > 0) { SVM.selectProject = context.Projects.OrderBy(p => p.title).FirstOrDefault(); SVM.projects = context.Projects.OrderBy(p => p.title).ToList(); }
                else { return RedirectToAction("Index", "Home"); }
            }
            else {

                SVM.selectProject = context.Projects.Where(p => p.title == project).FirstOrDefault();
                SVM.projects = context.Projects.OrderBy(p => p.title).ToList();
            }
            SVM.countOfTests = context.Scenarios.Where(s => s.Projectid == SVM.selectProject.id).Count();
            SVM.countWithSuccessCompleet = context.Scenarios.Where(s => s.Projectid == SVM.selectProject.id && s.typeOfError == "Не летально").Count();
            SVM.countWithUnSuccessCompleet = context.Scenarios.Where(s => s.Projectid == SVM.selectProject.id && s.typeOfError != "Не летально").Count();
            SVM.countOfFixedErreros = context.Scenarios.Where(s => s.Projectid == SVM.selectProject.id && s.isFixed == true).Count();
            SVM.countOfWaitingToFix = context.Scenarios.Where(s => s.Projectid == SVM.selectProject.id && s.isFixed == false).Count();
            
            return View(SVM);
            
        }

        [HttpGet]
        public ActionResult AddProject(Project pr)
        {

            return View(pr);
        }
        [HttpPost]
        public ActionResult AddProject(string title)
        {
            if (String.IsNullOrEmpty(title)) { return RedirectToAction("AddProject"); }
            Project pr = new Project();
            pr.title = title;
            if (context.Projects.Where(p => p.title == pr.title).Count() > 0) return RedirectToAction("AddProject", pr);
            context.Projects.Add(pr);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProject(int? id)
        {
            if (id == null) { return RedirectToAction("Index", "Home"); }
            Project pr = context.Projects.Where(p => p.id == id).FirstOrDefault();
            pr.Scenarios = context.Scenarios.Include(s => s.Actions).Where(s => s.Projectid == id).ToList();
            context.Projects.Remove(pr);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CreateReport(int? id)
        {
            if (id == null) { return RedirectToAction("Index", "Home"); }
            using (XLWorkbook wb = new XLWorkbook(XLEventTracking.Disabled))
            {
                Project pr = context.Projects.Include(p => p.Scenarios).Where(p => p.id == id).FirstOrDefault();
                foreach (var item in pr.Scenarios)
                {
                    var workheet = wb.Worksheets.Add(item.title);
                    workheet.Cell(1, 1).Value = "Id";
                    workheet.Cell(1, 2).Value = "Название";
                    workheet.Cell(1, 3).Value = "Результат";
                    workheet.Cell(1, 4).Value = "Дата проведения теста";
                    workheet.Cell(1, 5).Value = "Тип ошибки";
                    workheet.Cell(1, 6).Value = "Дата исправления";
                    workheet.Cell(1, 7).Value = "Имя ответсвенного сотрудника";
                    workheet.Cell(1, 8).Value = "Состояние исправления";

                    workheet.Cell(2, 1).Value = item.id;
                    workheet.Cell(2, 2).Value = item.title;
                    workheet.Cell(2, 3).Value = item.result;
                    workheet.Cell(2, 4).Value = item.dateOfExecution;
                    workheet.Cell(2, 5).Value = item.typeOfError;
                    workheet.Cell(2, 6).Value = item.dateOfBugFix;
                    workheet.Cell(2, 7).Value = item.nameOfFixer;
                    if (item.isFixed == false)
                        workheet.Cell(2, 8).Value = "не исправлен";
                    else workheet.Cell(2, 8).Value = "Исправлен";
                    item.Actions = context.Actions.Where(a => a.Scenarioid == item.id).ToList();
                    workheet.Cell(3, 1).Value = "№";
                    workheet.Cell(3, 2).Value = "Действие";
                    int i = 4;
                    foreach (var itemAction in item.Actions)
                    {
                        workheet.Cell(i, 1).Value = itemAction.id;
                        workheet.Cell(i, 2).Value = itemAction.action;
                        i++;
                    }
                    
                    workheet.Columns().AdjustToContents();

                }
                

                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
                        FileDownloadName = pr.title+".xlsx"
                    
                    };
                }
            
            }
            
        }

        
    }
}
using BugTracker.BusinessLogic;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        [Route("projects")]
        public ActionResult ViewProjects()
        {
            var data = ProjectProcessor.LoadProjects();
            List<ProjectsAllDataModel> projects = new List<ProjectsAllDataModel>();

            foreach (var row in data)
            {
                projects.Add(new ProjectsAllDataModel
                {
                    ProjectId = row.ProjectId,
                    ProjectName = row.ProjectName,
                    Description = row.Description,
                    TicketsNumber = row.TicketsNumber,
                    WorkersNumber = row.WorkersNumber
                });
            }
            ViewBag.ActiveMenu = "Projects";
            return View(projects);
        }

        public IActionResult AddProject()
        {
            ViewBag.Message = "Add Project";
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = ProjectProcessor.CreateProject(
                    model.ProjectName,
                    model.Description);

                ViewBag.ActiveMenu = "Projects";
                return RedirectToAction("ViewProjects");
            }
            return View();
        }
    }
}

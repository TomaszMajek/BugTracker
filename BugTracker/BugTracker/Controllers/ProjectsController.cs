using BugTracker.BusinessLogic;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
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
            var data = ProjectProcessor.LoadTickets();
            List<ProjectModel> projects = new List<ProjectModel>();

            foreach (var row in data)
            {
                projects.Add(new ProjectModel
                {
                    ProjectId = row.ProjectId,
                    ProjectName = row.ProjectName,
                    Description = row.Description
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

using BugTracker.BusinessLogic;
using BugTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public class GlobalVariables
        {
            public static int selectedProject { get; set; }
            private static readonly Dictionary<int, int> justToInit = new Dictionary<int, int>();
            public static SelectList selectedList { get; set; } = new SelectList(justToInit, "Key", "Value", 0);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.ActiveMenu = "Index";
            return View();
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties()
            {
                RedirectUri = Url.Action("Index", "Home")
            });

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            ViewBag.ActiveMenu = "Dashboard";
            return View();
        }

        [Route("tickets")]
        public ActionResult ViewTickets(int projectId)
        {
            var projectData = ProjectProcessor.GetProjectsNames();

            // solves problem of reseting "choosing project" dropdownlist after reload
            try
            {
                if (GlobalVariables.selectedProject == 0 && Convert.ToInt16(GlobalVariables.selectedList.SelectedValue) > 0)
                {
                    projectId = Convert.ToInt16(GlobalVariables.selectedList.SelectedValue);
                }
                else
                {
                    projectId = GlobalVariables.selectedProject;
                    GlobalVariables.selectedProject = Int16.Parse(Request.Form["id"]);
                    projectId = GlobalVariables.selectedProject;
                }
            }
            catch (Exception e)
            {

            }
            
            var dictionary = projectData.ToDictionary(x => x.ProjectId, x => x.ProjectName);
            var data = TicketProcessor.LoadTickets(projectId);

            List<TicketModel> tickets = new List<TicketModel>();

            foreach (var row in data)
            {
                tickets.Add(new TicketModel
                {
                    Id = row.Id,
                    BugName = row.BugName,
                    Description = row.Description,
                    Reporter = row.Reporter,
                    Created = row.Created,
                    Deadline = row.Deadline,
                    Status = row.Status,
                    Severity = row.Severity
                });
            }

            // Keeping selected value after submiting (changing project)
            GlobalVariables.selectedList = new SelectList(dictionary, "Key", "Value", GlobalVariables.selectedProject);
            ViewBag.ActiveMenu = "Tickets";
            ViewBag.ProjectName = GlobalVariables.selectedList;
            
            return View(tickets);
        }

        // actually should be AddTicket
        public IActionResult AddBug()
        {
            ViewBag.Message = "Add Ticket";
            return View();
        }

        [HttpPost]
        public IActionResult AddBug(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = TicketProcessor.CreateTicket(
                    model.BugName, 
                    model.Description, 
                    model.Reporter, 
                    model.Created, 
                    model.Deadline, 
                    model.Status,
                    model.Severity,
                    model.ProjectId);

                ViewBag.ActiveMenu = "ViewTickets";
                return RedirectToAction("ViewTickets"); 
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBug(int Id)
        {
            TicketProcessor.DeleteTicket(Id);

            ViewBag.ActiveMenu = "ViewTickets";
            return RedirectToAction("ViewTickets");
        }

        
        public ActionResult DetailsBug(int Id)
        {
            var data = TicketProcessor.FindTicket(Id);
            TicketModel ticket = new TicketModel();

            ticket.Id = data[0].Id;
            ticket.BugName = data[0].BugName;
            ticket.Description = data[0].Description;
            ticket.Reporter = data[0].Reporter;
            ticket.Created = data[0].Created;
            ticket.Deadline = data[0].Deadline;
            ticket.Status = data[0].Status;
            ticket.Severity = data[0].Severity;

            return View(ticket);
        }


        public ActionResult EditBug(int Id)
        {
            var data = TicketProcessor.FindTicket(Id);
            TicketModel ticket = new TicketModel();

            ticket.Id = data[0].Id;
            ticket.BugName = data[0].BugName;
            ticket.Description = data[0].Description;
            ticket.Reporter = data[0].Reporter;
            ticket.Created = data[0].Created;
            ticket.Deadline = data[0].Deadline;
            ticket.Status = data[0].Status;
            ticket.Severity = data[0].Severity;

            return View(ticket);
        }

        [HttpPost]
        public IActionResult EditSaveBug(TicketModel model)
        {
            var data = TicketProcessor.EditTicket(
                model.Id, 
                model.BugName, 
                model.Description,
                model.Reporter,
                model.Created,
                model.Deadline,
                model.Status,
                model.Severity
            );

            ViewBag.ActiveMenu = "ViewTickets";
            return RedirectToAction("ViewTickets");
        }


    }
}
 
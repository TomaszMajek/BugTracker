using BugTracker.BusinessLogic;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public ActionResult ViewTickets()
        {
            ViewBag.Message = "Tickets List";
            var data = TicketProcessor.LoadTickets();
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
                    model.Severity);

                return RedirectToAction("ViewTickets"); 
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteBug(int Id)
        {
            TicketProcessor.DeleteTicket(Id);

            return RedirectToAction("ViewTickets");
        }
    }
}
 
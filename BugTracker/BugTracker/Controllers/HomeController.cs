﻿using BugTracker.BusinessLogic;
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
            ViewBag.ActiveMenu = "Index";
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

        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            ViewBag.ActiveMenu = "Dashboard";
            return View();
        }

        [Route("tickets")]
        public ActionResult ViewTickets(int projectId)
        {
            int projectIdAction = 1;
            var data = TicketProcessor.LoadTickets(projectIdAction);
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
            ViewBag.ActiveMenu = "Tickets";
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
 
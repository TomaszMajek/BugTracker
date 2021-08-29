﻿using BugTracker.DataAccess;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.BusinessLogic
{
    public static class TicketProcessor
    {
        public static int CreateTicket(string bugName, string description, 
            string reporter, System.DateTime? created, System.DateTime? deadline, 
            string status, string severity)
        {
            TicketModel data = new TicketModel
            {
                BugName = bugName,
                Description = description,
                Reporter = reporter,
                Created = created,
                Deadline = deadline,
                Status = status,
                Severity = severity
            };

            string sql = @"insert into dbo.Ticket (BugName, Description, Reporter, Created, Deadline, Status, Severity)
                            values (@BugName, @Description, @Reporter, @Created, @Deadline, @Status, @Severity);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<TicketModel> LoadTickets()
        {
            string sql = @"select Id, BugName, Description, Reporter, Created, Deadline, Status, Severity
                            from dbo.Ticket;";

            return SqlDataAccess.LoadData<TicketModel>(sql);
        }
    }
}
using BugTracker.DataAccess;
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
            string reporter, System.DateTime created, System.DateTime deadline, 
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


        public static int DeleteTicket(int Id)
        {
            int data = Id;

            string sql = $"delete from dbo.Ticket where Id = {data}";

            return SqlDataAccess.RemoveData(sql, data);
        }


        public static List<TicketModel> FindTicket(int Id)
        {
            int data = Id;
            
            string sql = $"select * from dbo.Ticket where Id = {data}";

            return SqlDataAccess.FindData<TicketModel>(sql);
        }

        public static int EditTicket(int id, string bugName, string description,
            string reporter, System.DateTime created, System.DateTime deadline,
            string status, string severity)
        {
            TicketModel data = new TicketModel
            {
                Id = id,
                BugName = bugName,
                Description = description,
                Reporter = reporter,
                Created = created,
                Deadline = deadline,
                Status = status,
                Severity = severity
            };

            string sql = @"update dbo.Ticket set BugName = @BugName, Description = @Description, Reporter = @Reporter, Created = @Created, Deadline = @Deadline, Status = @Status, Severity = @Severity where Id = @id";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}

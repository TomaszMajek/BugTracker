using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class TicketAndProjectModel
    {
        public int Id { get; set; }
        public string BugName { get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Severity { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    }
}

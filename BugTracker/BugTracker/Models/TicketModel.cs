using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class TicketModel
    {
        public int TicketId { get; set; }
        public string BugName { get; set; }
        public string Reporter { get; set; }
        public string Created { get; set; }
        public string Due { get; set; }
        public string Status { get; set; }
        public string Severity { get; set; }
    }
}

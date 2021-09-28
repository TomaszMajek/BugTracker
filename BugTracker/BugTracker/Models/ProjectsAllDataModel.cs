using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public partial class ProjectsAllDataModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string TicketNumber { get; set; }
        public string WorkersNumber { get; set; }
    }
}

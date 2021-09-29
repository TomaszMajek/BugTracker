using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public partial class ProjectsAllDataModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tickets number")]
        public string TicketsNumber { get; set; }

        [Display(Name = "Workers number")]
        public string WorkersNumber { get; set; }
    }
}

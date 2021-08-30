using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BugTracker.Models
{
    public partial class TicketModel
    {
        public int Id { get; set; }

        [Display(Name = "Bug Name")]
        [Required(ErrorMessage = "You must name the problem.")]
        public string BugName { get; set; }

        [Display(Name = "Description (optional)")]
        public string Description { get; set; }

        [Display(Name = "Reporter")]
        public string Reporter { get; set; }

        [Display(Name = "Created")]
        public DateTime? Created { get; set; }

        [Display(Name = "Deadline")]
        [Required(ErrorMessage = "You need to write by when the problem should be solved.")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "You must select status. Is the problem in progress already or maybe almost solved?")]
        public string Status { get; set; }

        [Display(Name = "Severity")]
        [Required(ErrorMessage = "You have to set prirority.")]
        public string Severity { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BugTracker.Models
{
    public partial class ProjectModel
    {
        [Key]
        public int ProjectId { get; set; }

        [Display(Name = "Project name")]
        [Required(ErrorMessage = "You must name the project.")]
        public string ProjectName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Write something about the project.")]
        public string Description { get; set; }
    }
}

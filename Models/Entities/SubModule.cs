﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ScrumApp__Juro_.Models.Entities
{
    public class SubModule
    {
        public int SubModuleID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProjectID { get; set; }
        public int DeveloperID { get; set; }
        public int ModuleID { get; set; }
        [ValidateNever]
        public virtual Module Module { get; set; }
        [ValidateNever]
        public virtual ICollection<Task> Tasks { get; set; }
    }

}

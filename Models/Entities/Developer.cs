﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Developer
    {
        public int DeveloperID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }

        // Foreign Key

    }
}

﻿using System.ComponentModel.DataAnnotations;
using WebApiDemo.Models.Validations;

namespace WebApiDemo.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Color { get; set; }
        public string? Description { get; set; }
        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public int? Price { get; set; }
        
        public bool ValidateDescription()
        {
            return !string.IsNullOrEmpty(Description);
        }
    }
}

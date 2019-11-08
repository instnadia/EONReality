using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EONReality.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Display(Name = "Date Registred")]
        [Required]
        public DateTime DRegister { get; set; }
        public string Dates { get; set; }
        [DataType(DataType.Text)]
        public string ARequest { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
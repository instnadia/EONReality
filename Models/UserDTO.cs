using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EONReality.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DRegister { get; set; }
        public string Dates { get; set; }
        public string ARequest { get; set; }
    }
}
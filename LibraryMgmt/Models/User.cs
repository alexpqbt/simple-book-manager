using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.Models
{
    internal class User
    {
        public int UserId { get; set; }
        public int SchoolId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

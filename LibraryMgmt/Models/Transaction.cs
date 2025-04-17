using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.Models
{
    internal class Transaction
    {
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int SchoolId { get; set; }
        public string? BorrowerName { get; set; }
        public string? BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Status { get; set; }
    }
}

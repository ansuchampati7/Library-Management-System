using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library_Management_System.Domain.Entities
{
    public class Borrower
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}

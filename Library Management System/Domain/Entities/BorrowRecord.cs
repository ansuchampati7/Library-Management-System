using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }

}

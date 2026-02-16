using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library_Management_System.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public int PublishedYear { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int TotalQuantity { get; set; }
        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}

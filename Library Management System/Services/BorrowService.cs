using Library_Management_System.Domain.Entities;
using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly LibraryDbContext _context;

        public BorrowService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task BorrowBookAsync(string bookTittle, string borrowerName)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Title == bookTittle.ToLower());

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("All Books are already borrowed");
                return;
            }

            var borrower = await _context.Borrowers.FirstOrDefaultAsync(x => x.Name == borrowerName.ToLower());

            if (borrower == null)
            {
                borrower = new Borrower
                {
                    Name = borrowerName.ToLower(),
                    PhoneNumber = "N/A"
                };
                _context.Borrowers.Add(borrower);
                await _context.SaveChangesAsync();
            }
            book.TotalQuantity--;
            book.IsAvailable = book.TotalQuantity > 0 ? true : false;

            var record = new BorrowRecord
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                BorrowedDate = DateTime.UtcNow
            };

            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();
            Console.WriteLine("Book Borrowed!");
        }

        public async Task ReturnBookAsync(string bookTitle, string authorName, string borrowerName)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(x => x.Name == borrowerName.ToLower());

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found");
                return;
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(x => x.Title == bookTitle.ToLower() && x.Author.Name == authorName.ToLower());

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            var borrowRecord = await _context.BorrowRecords.FirstOrDefaultAsync(x =>
                x.BorrowerId == borrower.Id &&
                x.BookId == book.Id &&
                x.ReturnedDate == null);

            if (borrowRecord == null)
            {
                Console.WriteLine("No active borrow record found for this borrower and book");
                return;
            }

            borrowRecord.ReturnedDate = DateTime.UtcNow;
            book.TotalQuantity++;
            book.IsAvailable = true;

            await _context.SaveChangesAsync();
            Console.WriteLine("Book Returned!");
        }

        public async Task<List<BorrowRecord>> GetAllBorrowedBooksAsync()
        {
            var borrowedBooks = await _context.BorrowRecords
                .Include(br => br.Book)
                .ThenInclude(b => b.Author)
                .Include(br => br.Borrower)
                .Where(br => br.ReturnedDate == null)
                .ToListAsync();

            return borrowedBooks;
        }
    }

}

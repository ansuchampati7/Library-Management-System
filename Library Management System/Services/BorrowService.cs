using Library_Management_System.Domain.Entities;
using Library_Management_System.Repository.Interfaces;
using Library_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IBorrowRecordRepository _borrowRecordRepository;

        public BorrowService(IBookRepository bookRepository, IBorrowerRepository borrowerRepository, IBorrowRecordRepository borrowRecordRepository)
        {
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
            _borrowRecordRepository = borrowRecordRepository;
        }

        public async Task BorrowBookAsync(string bookTittle, string borrowerName)
        {
            var book = await _bookRepository.GetBookByTitleAsync(bookTittle);

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

            var borrower = await _borrowerRepository.GetBorrowerByNameAsync(borrowerName);

            if (borrower == null)
            {
                borrower = new Borrower
                {
                    Name = borrowerName.ToLower(),
                    PhoneNumber = "N/A"
                };
                await _borrowerRepository.AddAsync(borrower);
                await _borrowerRepository.SaveChangesAsync();
            }
            book.TotalQuantity--;
            book.IsAvailable = book.TotalQuantity > 0 ? true : false;
            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();

            var record = new BorrowRecord
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                BorrowedDate = DateTime.UtcNow
            };

            await _borrowRecordRepository.AddAsync(record);
            await _borrowRecordRepository.SaveChangesAsync();
            Console.WriteLine("Book Borrowed!");
        }

        public async Task ReturnBookAsync(string bookTitle, string authorName, string borrowerName)
        {
            var borrower = await _borrowerRepository.GetBorrowerByNameAsync(borrowerName);

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found");
                return;
            }

            var book = await _bookRepository.GetBookByTitleAndAuthorAsync(bookTitle, authorName);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            var borrowRecord = await _borrowRecordRepository.GetActiveBorrowRecordAsync(borrower.Id, book.Id);

            if (borrowRecord == null)
            {
                Console.WriteLine("No active borrow record found for this borrower and book");
                return;
            }

            book.TotalQuantity++;
            book.IsAvailable = true;
            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();

            borrowRecord.ReturnedDate = DateTime.UtcNow;
            await _borrowRecordRepository.SaveChangesAsync();
            Console.WriteLine("Book Returned!");
        }

        public async Task<List<BorrowRecord>> GetAllBorrowedBooksAsync()
        {
            return await _borrowRecordRepository.GetAllActiveBorrowRecordsAsync();
        }
    }
}

using Library_Management_System.Domain.Entities;
using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Implementations
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRecordRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BorrowRecord entity)
        {
            await _context.BorrowRecords.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<BorrowRecord> GetActiveBorrowRecordAsync(int borrowerId, int bookId)
        {
            return await _context.BorrowRecords.FirstOrDefaultAsync(x =>
                x.BorrowerId == borrowerId &&
                x.BookId == bookId &&
                x.ReturnedDate == null);
        }

        public async Task<List<BorrowRecord>> GetAllActiveBorrowRecordsAsync()
        {
            return await _context.BorrowRecords
                .Include(br => br.Book)
                .ThenInclude(b => b.Author)
                .Include(br => br.Borrower)
                .Where(br => br.ReturnedDate == null)
                .ToListAsync();
        }
    }
}

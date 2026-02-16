using Library_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Interfaces
{
    public interface IBorrowRecordRepository
    {
        Task AddAsync(BorrowRecord entity);
        Task<int> SaveChangesAsync();
        Task<BorrowRecord> GetActiveBorrowRecordAsync(int borrowerId, int bookId);
        Task<List<BorrowRecord>> GetAllActiveBorrowRecordsAsync();
    }
}

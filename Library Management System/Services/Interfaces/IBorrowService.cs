using System;
using System.Collections.Generic;
using System.Text;
using Library_Management_System.Domain.Entities;

namespace Library_Management_System.Services.Interfaces
{
    public interface IBorrowService
    {
        Task BorrowBookAsync(string bookTittle, string borrowerName);
        Task ReturnBookAsync(string bookTitle, string authorName, string borrowerName);
        Task<List<BorrowRecord>> GetAllBorrowedBooksAsync();
    }

}

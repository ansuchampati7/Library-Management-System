using Library_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Services.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(string title, string authorName);
        Task<List<Book>> GetAllBooksAsync();
        Task DeleteBookAsync(string tittle, string author);
    }

}

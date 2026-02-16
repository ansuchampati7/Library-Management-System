using Library_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Interfaces
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetBookByTitleAndAuthorAsync(string title, string authorName);
        Task<Book> GetBookByTitleAsync(string title);
    }
}

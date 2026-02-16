using Library_Management_System.Domain.Entities;
using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Implementations
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author)
                .ToListAsync(); ;
        }

        public async Task<Book> GetBookByTitleAndAuthorAsync(string title, string authorName)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(a => a.Title == title.ToLower() && a.Author.Name == authorName.ToLower());
        }

        public async Task<Book> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Title == title.ToLower());
        }
    }
}

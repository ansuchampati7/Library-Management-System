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
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author)
                .ToListAsync(); ;
        }

        public async Task AddAsync(Book entity)
        {
            await _context.Books.AddAsync(entity);
        }

        public void Update(Book entity)
        {
            _context.Books.Update(entity);
        }

        public void Remove(Book entity)
        {
            _context.Books.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
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

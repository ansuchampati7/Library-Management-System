using Library_Management_System.Domain.Entities;
using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(string title, string authorName)
        {
            var bookExists = await _context.Books
                    .FirstOrDefaultAsync(a => a.Title == title.ToLower() && a.Author.Name == authorName.ToLower());

            if (bookExists != null)
            {
                bookExists.TotalQuantity++;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    Console.WriteLine("Author name is required");
                    return;
                }
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Name == authorName.ToLower());

                if (author == null)
                {
                    author = new Author
                    {
                        Name = authorName.ToLower()
                    };

                    await _context.Authors.AddAsync(author);
                    await _context.SaveChangesAsync();
                }

                var book = new Book
                {
                    Title = title.ToLower(),
                    AuthorId = author.Id,
                    IsAvailable = true,
                    TotalQuantity = 1
                };

                await _context.Books.AddAsync(book);
                Console.WriteLine("Book Added!");
            }
            await _context.SaveChangesAsync();
        }


        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task DeleteBookAsync(string tittle, string author)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Title == tittle.ToLower() && x.Author.Name == author.ToLower());

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                Console.WriteLine("Book Deleted!");
            }   
        }
    }
}

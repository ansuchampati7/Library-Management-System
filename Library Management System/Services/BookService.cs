using Library_Management_System.Domain.Entities;
using Library_Management_System.Repository.Interfaces;
using Library_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_Management_System.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task AddBookAsync(string title, string authorName)
        {
            var bookExists = await _bookRepository.GetBookByTitleAndAuthorAsync(title, authorName);

            if (bookExists != null)
            {
                bookExists.TotalQuantity++;
                _bookRepository.Update(bookExists);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    Console.WriteLine("Author name is required");
                    return;
                }
                var author = await _authorRepository.GetAuthorByNameAsync(authorName);

                if (author == null)
                {
                    author = new Author
                    {
                        Name = authorName.ToLower()
                    };

                    await _authorRepository.AddAsync(author);
                    await _authorRepository.SaveChangesAsync();
                }

                var book = new Book
                {
                    Title = title.ToLower(),
                    AuthorId = author.Id,
                    IsAvailable = true,
                    TotalQuantity = 1
                };

                await _bookRepository.AddAsync(book);
                Console.WriteLine("Book Added!");
            }
            await _bookRepository.SaveChangesAsync();
        }


        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.ToList();
        }

        public async Task DeleteBookAsync(string tittle, string author)
        {
            var book = await _bookRepository.GetBookByTitleAndAuthorAsync(tittle, author);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
            else
            {
                _bookRepository.Remove(book);
                await _bookRepository.SaveChangesAsync();
                Console.WriteLine("Book Deleted!");
            }   
        }
    }
}

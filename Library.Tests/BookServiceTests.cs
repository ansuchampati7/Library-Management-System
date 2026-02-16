using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Library_Management_System.Domain.Entities;
using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Repository.Implementations;
using Library_Management_System.Services;

namespace Library.Tests
{
    public class BookServiceTests
    {
        private LibraryDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LibraryDbContext(options);
        }

        [Fact]
        public async Task AddBook_Should_Increase_Book_Count()
        {

            var context = GetInMemoryDbContext();
            var bookRepository = new BookRepository(context);
            var authorRepository = new AuthorRepository(context);
            var service = new BookService(bookRepository, authorRepository);


            await service.AddBookAsync("Clean Code", "Robert Martin");


            var bookCount = await context.Books.CountAsync();
            Assert.Equal(1, bookCount);
        }

        [Fact]
        public async Task AddBook_Should_Create_Author_If_Not_Exists()
        {

            var context = GetInMemoryDbContext();
            var bookRepository = new BookRepository(context);
            var authorRepository = new AuthorRepository(context);
            var service = new BookService(bookRepository, authorRepository);


            await service.AddBookAsync("Clean Code", "Robert Martin");


            var authorCount = await context.Authors.CountAsync();
            Assert.Equal(1, authorCount);
        }

        [Fact]
        public async Task AddBook_Should_Increase_TotalQuantity_If_Book_Exists()
        {

            var context = GetInMemoryDbContext();
            var bookRepository = new BookRepository(context);
            var authorRepository = new AuthorRepository(context);
            var service = new BookService(bookRepository, authorRepository);

            await service.AddBookAsync("Clean Code", "Robert Martin");


            await service.AddBookAsync("Clean Code", "Robert Martin");


            var book = await context.Books.FirstAsync();
            Assert.Equal(1, await context.Books.CountAsync());
            Assert.Equal(2, book.TotalQuantity);
        }
    }
}

using Library_Management_System.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library_Management_System.Services
{
    public class LibraryApplicationService : ILibraryApplicationService
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        private readonly ILogger<LibraryApplicationService> _logger;

        public LibraryApplicationService(
            IBookService bookService,
            IBorrowService borrowService,
            ILogger<LibraryApplicationService> logger)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _borrowService = borrowService ?? throw new ArgumentNullException(nameof(borrowService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("*******************************");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. List Books");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. View Borrowed Books");
                Console.WriteLine("7. Exit");
                Console.WriteLine("*******************************");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await HandleAddBookAsync();
                            break;

                        case "2":
                            await HandleListBooksAsync();
                            break;

                        case "3":
                            await HandleBorrowBookAsync();
                            break;

                        case "4":
                            await HandleDeleteBookAsync();
                            break;

                        case "5":
                            await HandleReturnBookAsync();
                            break;

                        case "6":
                            await HandleListBorrowedBooksAsync();
                            break;

                        case "7":
                            Console.WriteLine("******BYE!******");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred");
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private async Task HandleAddBookAsync()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Book title cannot be null or have white space only");
                return;
            }

            Console.Write("Author Name: ");
            var authorName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(authorName))
            {
                Console.WriteLine("Author Name of the book cannot be null or have white space only");
                return;
            }

            await _bookService.AddBookAsync(title, authorName);
            Console.WriteLine("Book added successfully.");
        }

        private async Task HandleListBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            if (books.Count == 0)
            {
                Console.WriteLine("No book available");
            }
            else
            {
                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Title} - Available: {b.IsAvailable} - Quantity: {b.TotalQuantity} - Author Name: {b.Author.Name}");
                }
            }
        }

        private async Task HandleBorrowBookAsync()
        {
            Console.Write("Book Title: ");
            string bookTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                Console.WriteLine("Book title cannot be null or have white space only");
                return;
            }

            Console.Write("Borrower Name: ");
            string borrowerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(borrowerName))
            {
                Console.WriteLine("Borrower Name cannot be null or have white space only");
                return;
            }

            await _borrowService.BorrowBookAsync(bookTitle, borrowerName);
            Console.WriteLine("Book borrowed successfully.");
        }

        private async Task HandleDeleteBookAsync()
        {
            Console.Write("Book Title: ");
            string bookTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                Console.WriteLine("Book title cannot be null or have white space only");
                return;
            }

            Console.Write("Author Name: ");
            var authorName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(authorName))
            {
                Console.WriteLine("Author Name of the book cannot be null or have white space only");
                return;
            }

            await _bookService.DeleteBookAsync(bookTitle, authorName);
            Console.WriteLine("Book deleted successfully.");
        }

        private async Task HandleReturnBookAsync()
        {
            Console.Write("Borrower Name: ");
            string borrowerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(borrowerName))
            {
                Console.WriteLine("Borrower Name cannot be null or have white space only");
                return;
            }

            Console.Write("Book Title: ");
            string bookTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                Console.WriteLine("Book title cannot be null or have white space only");
                return;
            }

            Console.Write("Author Name: ");
            string authorName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(authorName))
            {
                Console.WriteLine("Author Name cannot be null or have white space only");
                return;
            }

            await _borrowService.ReturnBookAsync(bookTitle, authorName, borrowerName);
            Console.WriteLine("Book returned successfully.");
        }

        private async Task HandleListBorrowedBooksAsync()
        {
            var borrowedBooks = await _borrowService.GetAllBorrowedBooksAsync();
            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No borrowed books available");
            }
            else
            {
                Console.WriteLine("\n**** Currently Borrowed Books ****");
                foreach (var record in borrowedBooks)
                {
                    Console.WriteLine($"Book: {record.Book.Title} - Author: {record.Book.Author.Name} - Borrower: {record.Borrower.Name} - Borrowed Date: {record.BorrowedDate:yyyy-MM-dd HH:mm:ss}");
                }
                Console.WriteLine("***********************************\n");
            }
        }
    }
}

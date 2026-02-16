using System.ComponentModel.DataAnnotations;
using Library_Management_System.Domain.Entities;
using Xunit;

namespace Library.Tests
{
    public class AuthorTests
    {
        [Fact]
        public void Author_Should_Be_Invalid_When_Name_Is_Missing()
        {
            
            var author = new Author();

            var context = new ValidationContext(author);
            var results = new List<ValidationResult>();

            
            var isValid = Validator.TryValidateObject(author, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Author_Should_Be_Invalid_When_Name_Exceeds_MaxLength()
        {
            
            var author = new Author
            {
                Name = new string('A', 101)
            };

            var context = new ValidationContext(author);
            var results = new List<ValidationResult>();

            
            var isValid = Validator.TryValidateObject(author, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Author_Should_Be_Valid_When_Name_Is_Proper()
        {
            
            var author = new Author
            {
                Name = "J.K. Rowling"
            };

            var context = new ValidationContext(author);
            var results = new List<ValidationResult>();

            
            var isValid = Validator.TryValidateObject(author, context, results, true);

            
            Assert.True(isValid);
        }
    }
}

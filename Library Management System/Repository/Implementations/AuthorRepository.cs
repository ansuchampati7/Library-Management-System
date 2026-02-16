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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author entity)
        {
            await _context.Authors.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name.ToLower());
        }
    }
}

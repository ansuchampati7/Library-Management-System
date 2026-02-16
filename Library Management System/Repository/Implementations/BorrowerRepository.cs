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
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowerRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Borrower entity)
        {
            await _context.Borrowers.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Borrower> GetBorrowerByNameAsync(string name)
        {
            return await _context.Borrowers.FirstOrDefaultAsync(x => x.Name == name.ToLower());
        }
    }
}

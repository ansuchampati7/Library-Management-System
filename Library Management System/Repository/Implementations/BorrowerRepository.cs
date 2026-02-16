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
    public class BorrowerRepository : RepositoryBase<Borrower>, IBorrowerRepository
    {
        public BorrowerRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Borrower> GetBorrowerByNameAsync(string name)
        {
            return await _context.Borrowers.FirstOrDefaultAsync(x => x.Name == name.ToLower());
        }
    }
}

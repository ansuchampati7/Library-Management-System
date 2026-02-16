using Library_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Interfaces
{
    public interface IBorrowerRepository
    {
        Task AddAsync(Borrower entity);
        Task<int> SaveChangesAsync();
        Task<Borrower> GetBorrowerByNameAsync(string name);
    }
}

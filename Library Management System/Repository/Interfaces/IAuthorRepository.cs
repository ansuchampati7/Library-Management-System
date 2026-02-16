using Library_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author entity);
        Task<int> SaveChangesAsync();
        Task<Author> GetAuthorByNameAsync(string name);
    }
}

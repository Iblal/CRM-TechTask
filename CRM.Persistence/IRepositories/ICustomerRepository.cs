using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Persistence.IRepositories
{

    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<bool> SaveChangesAsync();
    }

}

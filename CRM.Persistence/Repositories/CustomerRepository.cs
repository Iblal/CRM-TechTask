using CRM.Domain.Entities;
using CRM.Persistence.DbContexts;
using CRM.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CRMRelationalContext _context;

        public CustomerRepository(CRMRelationalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }

}

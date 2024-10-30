using CRM.Application.IServices;
using CRM.Models.BindingModels;
using CRM.Models.ViewModels;
using CRM.Domain.Entities;
using CRM.Persistence.IRepositories;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
    {
        var customers = await _repository.GetAllAsync();
        return customers.Select(c => new CustomerViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email
        });
    }

    public async Task<CustomerViewModel> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null)
        {
            return null;
        }

        return new CustomerViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email
        };
    }

    public async Task<bool> CreateCustomerAsync(CreateCustomerModel createCustomerModel)
    {
        var customer = new Customer(createCustomerModel.Name, createCustomerModel.Email);

        await _repository.AddAsync(customer);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateCustomerAsync(EditCustomerModel editCustomerModel)
    {
        var customer = await _repository.GetByIdAsync(editCustomerModel.Id);
        if (customer == null)
        {
            return false;
        }

        customer.Update(editCustomerModel.Name, editCustomerModel.Email);
        _repository.Update(customer);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null)
        {
            return false;
        }

        _repository.Delete(customer);
        return await _repository.SaveChangesAsync();
    }
}

using CRM.Models.BindingModels;
using CRM.Models.ViewModels;


namespace CRM.Application.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();

        Task<CustomerViewModel> GetCustomerByIdAsync(Guid id);

        Task<bool> CreateCustomerAsync(CreateCustomerModel createModel);

        Task<bool> UpdateCustomerAsync(EditCustomerModel updateModel);

        Task<bool> DeleteCustomerAsync(Guid id);
    }
}

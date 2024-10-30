using System.ComponentModel.DataAnnotations;

namespace CRM.Models.BindingModels
{
    public class CreateCustomerModel
    {
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer email is required.")]
        [EmailAddress(ErrorMessage = "Customer email is not in a valid format.")]
        public string Email { get; set; }
    }
}

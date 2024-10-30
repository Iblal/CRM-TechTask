// CustomersController.cs
using CRM.Application.IServices;
using CRM.Models.BindingModels;
using CRM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: Customers
    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return View(customers);
    }

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCustomerModel createDto)
    {
        if (ModelState.IsValid)
        {
            var success = await _customerService.CreateCustomerAsync(createDto);
            if (success)
                return RedirectToAction(nameof(Index));
        }
        return View(createDto);
    }

    // GET: Customers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);
        if (customer == null)
            return NotFound();

        var editCustomerModel = new EditCustomerModel
        {
            Name = customer.Name,
            Email = customer.Email
        };

        return View(editCustomerModel);
    }

    // POST: Customers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EditCustomerModel editCustomerModel)
    {
        if (id != editCustomerModel.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var success = await _customerService.UpdateCustomerAsync(editCustomerModel);
            if (success)
                return RedirectToAction(nameof(Index));
            // Handle update failure (e.g., concurrency issues)
        }
        return View(editCustomerModel);
    }

    // GET: Customers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // POST: Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var success = await _customerService.DeleteCustomerAsync(id);
        if (success)
            return RedirectToAction(nameof(Index));
        // Handle delete failure
        return RedirectToAction(nameof(Index));
    }
}

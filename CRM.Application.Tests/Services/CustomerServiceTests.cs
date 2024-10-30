using CRM.Application.IServices;
using CRM.Domain.Entities;
using CRM.Models.BindingModels;
using CRM.Persistence.IRepositories;
using Moq;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repositoryMock;
    private readonly ICustomerService _customerService;

    public CustomerServiceTests()
    {
        _repositoryMock = new Mock<ICustomerRepository>();
        _customerService = new CustomerService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ReturnsAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new Customer("John Doe", "john.doe@example.com"),
            new Customer("Jane Smith", "jane.smith@example.com")
        };
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

        // Act
        var result = await _customerService.GetAllCustomersAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("John Doe", result.First().Name);
        Assert.Equal("jane.smith@example.com", result.Last().Email);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_ReturnsCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = new Customer("John Doe", "john.doe@example.com");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(customer.Id)).ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetCustomerByIdAsync(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_ReturnsNull_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

        // Act
        var result = await _customerService.GetCustomerByIdAsync(customerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateCustomerAsync_AddsCustomer()
    {
        // Arrange
        var createCustomerModel = new CreateCustomerModel { Name = "John Doe", Email = "john.doe@example.com" };
        _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _customerService.CreateCustomerAsync(createCustomerModel);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateCustomerAsync_ReturnsFalse_WhenCustomerDoesNotExist()
    {
        // Arrange
        var editCustomerModel = new EditCustomerModel { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync(editCustomerModel.Id)).ReturnsAsync((Customer)null);

        // Act
        var result = await _customerService.UpdateCustomerAsync(editCustomerModel);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Never);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task DeleteCustomerAsync_DeletesCustomer_WhenCustomerExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer("John Doe", "john.doe@example.com");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync(customer);
        _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _customerService.DeleteCustomerAsync(customerId);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(repo => repo.Delete(It.IsAny<Customer>()), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteCustomerAsync_ReturnsFalse_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

        // Act
        var result = await _customerService.DeleteCustomerAsync(customerId);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(repo => repo.Delete(It.IsAny<Customer>()), Times.Never);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
    }
}


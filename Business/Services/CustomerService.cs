using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllCustomersAsync();
    }

    public async Task<CustomerEntity?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdAsync(id);
    }

    public async Task AddCustomerAsync(CustomerEntity customer)
    {
        await _customerRepository.AddCustomerAsync(customer);
    }

    public async Task UpdateCustomerAsync(CustomerEntity customer)
    {
        await _customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _customerRepository.DeleteCustomerAsync(id);
    }
}

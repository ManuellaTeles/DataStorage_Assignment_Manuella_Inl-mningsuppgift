using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<CustomerEntity?> GetCustomerByIdAsync(int id);
    Task AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task DeleteCustomerAsync(int id);
}

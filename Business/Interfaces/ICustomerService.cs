﻿using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<CustomerEntity?> GetCustomerByIdAsync(int id);
    Task AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task DeleteCustomerAsync(int id);
}

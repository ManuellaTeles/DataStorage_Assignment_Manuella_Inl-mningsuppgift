using Business.Interfaces;
using Data.Entities;

namespace ConsoleApp1.Dialogs;

public class CustomerDialog
{
    private readonly ICustomerService _customerService;

    public CustomerDialog(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ShowCustomerMenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Customers Menu:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Choose '1' to List all customers");
            Console.WriteLine("Choose '2' to Add a customer");
            Console.WriteLine("Choose '3' to Update a customer");
            Console.WriteLine("Choose '4' to Delete a customer");
            Console.WriteLine("Choose '5' to Return to the main menu");
            Console.WriteLine();
           
            Console.WriteLine();
            Console.Write("Select an option: ");



            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ListCustomersAsync();
                    break;
                case "2":
                    await AddCustomerAsync();
                    break;
                case "3":
                    await UpdateCustomerAsync();
                    break;
                case "4":
                    await DeleteCustomerAsync();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice, please try again!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task ListCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("List all customers:");
        Console.WriteLine("-------------------------------------");

        foreach (var customer in customers)
        {
            Console.WriteLine();
            Console.WriteLine($"Customer ID: {customer.Id}");  // Rättade "Custemer" -> "Customer"
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone number: {customer.PhoneNumber}");  // Rättade "Telefon number" -> "Phone number"
            Console.WriteLine("--------------------------------------------------------------------------");

        }

        
        Console.ReadKey();
    }

    private async Task AddCustomerAsync()
    {
        Console.Clear();
        Console.Write("Customer Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Customer Email: ");
        string email = Console.ReadLine() ?? "";
        Console.Write("Customer Phone number: ");
        string phoneNumber = Console.ReadLine() ?? "";

        var customer = new CustomerEntity { Name = name, Email = email, PhoneNumber = phoneNumber };
        await _customerService.AddCustomerAsync(customer);
        Console.WriteLine();
        Console.WriteLine("The customer has been added!");
        Console.WriteLine("Thank you");
        Console.ReadKey();
    }


    private async Task UpdateCustomerAsync()
    {
        Console.Clear();
        Console.WriteLine("Choose one of our customers to update:");
        Console.WriteLine("-------------------------------------");
       
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine();
            Console.WriteLine($"Customer ID: {customer.Id}");  // Rättade "Custemer" -> "Customer"
            Console.WriteLine($"Name: {customer.Name}");

        }
        Console.WriteLine();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine();
        Console.Write("Enter customer ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                Console.WriteLine();
                Console.Write($"New name ({customer.Name}): ");
                customer.Name = Console.ReadLine() ?? customer.Name;

                Console.Write($"New email ({customer.Email}): ");
                customer.Email = Console.ReadLine() ?? customer.Email;

                Console.Write($"New phone number ({customer.PhoneNumber}): ");
                customer.PhoneNumber = Console.ReadLine() ?? customer.PhoneNumber;
                Console.WriteLine();

                await _customerService.UpdateCustomerAsync(customer);
                Console.WriteLine("Customer updated!");
            }

            else
            {
                Console.WriteLine("No customer found!");
            }
        }
        
        Console.ReadKey();
    }

    private async Task DeleteCustomerAsync()
    {

        Console.Clear();
        Console.WriteLine("Choose one of our customers to update:");
        Console.WriteLine("-------------------------------------");
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine();
            Console.WriteLine($"Customer ID: {customer.Id}");  // Rättade "Custemer" -> "Customer"
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone number: {customer.PhoneNumber}");  // Rättade "Telefon number" -> "Phone number"
            Console.WriteLine("--------------------------------------------------------------------------");

        }
        Console.WriteLine();
        Console.Write("Enter customer ID to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine();

            await _customerService.DeleteCustomerAsync(id);
            Console.WriteLine("Customer deleted!");
            Console.WriteLine("Thanks");
        }
       
        Console.ReadKey();
    }
}
using Business.Interfaces;

namespace ConsoleApp1.Dialogs;

public class MenuDialog
{
    private readonly ICustomerService _customerService;
    private readonly IProjectService _projectService;

    public MenuDialog(ICustomerService customerService, IProjectService projectService)
    {
        _customerService = customerService;
        _projectService = projectService;
    }

    public async Task ShowMainMenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" #### Welcome to our App ####");
            Console.WriteLine();
            Console.WriteLine("Main Menu :");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Choose '1' To Manage Customers");
            Console.WriteLine("Choose '2' To Manage Projects");
            Console.WriteLine("Choose '3' Exit");
            Console.WriteLine();
            
            Console.WriteLine();
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await new CustomerDialog(_customerService).ShowCustomerMenuAsync();
                    break;
                case "2":
                    await new ProjectDialog(_projectService, _customerService).ShowProjectMenuAsync();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice, please try again!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

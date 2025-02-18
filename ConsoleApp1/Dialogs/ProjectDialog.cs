using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Data.Entities;

namespace ConsoleApp1.Dialogs;

public class ProjectDialog
{
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;

    public ProjectDialog(IProjectService projectService, ICustomerService customerService)
    {
        _projectService = projectService;
        _customerService = customerService;
    }

    public async Task ShowProjectMenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Project Menu:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Choose '1' to List all projects");
            Console.WriteLine("Choose '2' to Add a project");
            Console.WriteLine("Choose '3' to Update a Project");
            Console.WriteLine("Choose '4' to Delete a project");
            Console.WriteLine("Choose '5' to Return to the main menu");
            Console.WriteLine();

            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ListProjectsAsync();
                    break;
                case "2":
                    await AddProjectAsync();
                    break;
                case "3":
                    await UpdateProjectAsync();
                    break;
                case "4":
                    await DeleteProjectAsync();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task ListProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("List of all projects:");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine();

        if (!projects.Any())
        {
            Console.WriteLine("Inga projekt finns i systemet ännu.");
        }
        else
        {
            foreach (var project in projects)
            {
                Console.WriteLine();
                Console.WriteLine($"Project ID: {project.Id}");
                Console.WriteLine($"Project Number: {project.ProjectNumber}");
                Console.WriteLine($"Project Name: {project.ProjectName}");
                Console.WriteLine($"Project Manager: {project.ProjectManager}");
                Console.WriteLine($"Customer ID: {project.CustomerId}");
                Console.WriteLine($"Worked Hours: {project.HoursWorked}");
                Console.WriteLine($"Price for one hour: {project.HourlyRate} kr");
                Console.WriteLine($"Total Price: {project.TotalPrice} kr");
                Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
                Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Status: {(project.Status == 1 ? "Not started" : project.Status == 2 ? "Ongoing" : "Completed")}");
                Console.WriteLine("-------------------------------------------------------------------------------------");

            }

        }

        
        Console.ReadKey();
    }

    private async Task AddProjectAsync()
    {
        Console.Clear();

        var customers = await _customerService.GetAllCustomersAsync();
        if (!customers.Any())
        {
            Console.WriteLine("No customers found! Add a customer before creating a project.");
            Console.ReadKey();
            return;
        }

        Console.Write("Project Name: ");
        string projectName = Console.ReadLine() ?? "";

        Console.Write("Project Manager: ");
        string projectManager = Console.ReadLine() ?? "";

        Console.Write("Worked Hours: ");
        if (!int.TryParse(Console.ReadLine(), out int hoursWorked))
        {
            Console.WriteLine("Invalid number of hours, please try again.");
            return;
        }

        Console.Write("Price for one hour: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal ratePerHour))
        {
            Console.WriteLine("Invalid rate, please try again.");
            return;
        }

        Console.Write("Enter Start Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.WriteLine("Invalid date format, please try again.");
            return;
        }

        Console.Write("Enter End Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < startDate)
        {
            Console.WriteLine("End date must be after start date, please try again.");
            return;
        }

        Console.Write("Select Status (1=Not started, 2=Ongoing, 3=Completed): ");
        if (!int.TryParse(Console.ReadLine(), out int status) || status < 1 || status > 3)
        {
            Console.WriteLine("Invalid status selection, please try again.");
            return;
        }
        Console.WriteLine();
        Console.Write("Select a customer from the list:");
        Console.WriteLine();
        foreach (var customer in customers)
        {
            
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}");
        }
        Console.WriteLine();
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId) || !customers.Any(c => c.Id == customerId))
        {
            Console.WriteLine("Invalid customer ID! Please try again.");
            Console.ReadKey();
            return;
        }

        var project = new ProjectEntity
        {
            ProjectName = projectName,
            ProjectManager = projectManager,
            HoursWorked = hoursWorked,
            HourlyRate = ratePerHour,
            TotalPrice = hoursWorked * ratePerHour,
            StartDate = startDate,
            EndDate = endDate,
            Status = status,
            CustomerId = customerId
        };
        Console.WriteLine();
        await _projectService.AddProjectAsync(project);
        Console.WriteLine("Project added!");
        Console.WriteLine($"Your projekt number is : '{project.ProjectNumber}'.");
        Console.WriteLine($"The total price is: {project.TotalPrice}");
        Console.ReadKey();
    }

    private async Task UpdateProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("Our existing projects:");
        Console.WriteLine("-----------------------------------------------");

        var projects = await _projectService.GetAllProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found in the system.");
            Console.ReadKey();
            return;
        }

        foreach (var project in projects)
        {
            Console.WriteLine();
            Console.WriteLine($"Project ID: {project.Id}");
            Console.WriteLine($"Project Number: {project.ProjectNumber}");
            Console.WriteLine($"Project Name: {project.ProjectName}");
        }
        Console.WriteLine("-----------------------------------------------");

        Console.Write("Enter project ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project != null)
            {
                Console.Write($"New project name ({project.ProjectName}): ");
                string? projectName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(projectName))
                    project.ProjectName = projectName;

                Console.Write($"New manager ({project.ProjectManager}): ");
                string? projectManager = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(projectManager))
                    project.ProjectManager = projectManager;

                Console.Write($"New worked hours ({project.HoursWorked}): ");
                if (int.TryParse(Console.ReadLine(), out int newHoursWorked))
                    project.HoursWorked = newHoursWorked;

                Console.Write($"New price for one hour ({project.HourlyRate}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newRatePerHour))
                    project.HourlyRate = newRatePerHour;

                Console.Write($"New start date ({project.StartDate:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newStartDate))
                    project.StartDate = newStartDate;

                Console.Write($"New end date ({project.EndDate:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate) && newEndDate >= project.StartDate)
                    project.EndDate = newEndDate;
                else
                    Console.WriteLine("End date must be after start date. Keeping the old value.");

                Console.Write($"Update status ({(project.Status == 1 ? "Not started" : project.Status == 2 ? "Ongoing" : "Completed")}), (1=Not started, 2=Ongoing, 3=Completed): ");
                if (int.TryParse(Console.ReadLine(), out int newStatus) && newStatus >= 1 && newStatus <= 3)
                    project.Status = newStatus;

                // Recalculate the total price !!!!!!!!
                project.TotalPrice = project.HoursWorked * project.HourlyRate;

                await _projectService.UpdateProjectAsync(project);
                Console.WriteLine();
                Console.WriteLine("Project updated successfully!");
            }
            else
            {
                Console.WriteLine("No project found!");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID!");
        }
        Console.ReadKey();
    }


    private async Task DeleteProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("Our existing projects:");
        Console.WriteLine("-----------------------------------------------");

        var projects = await _projectService.GetAllProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found in the system.");
            Console.ReadKey();
            return;
        }
        foreach (var project in projects)
        {
            Console.WriteLine();
            Console.WriteLine($"Project ID: {project.Id}");
            Console.WriteLine($"Project Number: {project.ProjectNumber}");
            Console.WriteLine($"Project Name: {project.ProjectName}");
        }
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();
        Console.Write("Select a projekt-ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await _projectService.DeleteProjectAsync(id);
            Console.WriteLine("Project is deleted!");
        }
        else
        {
            Console.WriteLine("Wrong ID!");
        }
        Console.ReadKey();
    }
}

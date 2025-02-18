using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using ConsoleApp1.Dialogs;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jubra\source\repos\DataStorage_Assignment_Manuella_Inlämningsuppgift\Data\DataBases\DataStorage_Manuella.mdf;Integrated Security=True;Connect Timeout=30"));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProjectService, ProjectService>();

        services.AddTransient<MenuDialog>();
        services.AddTransient<CustomerDialog>();
        services.AddTransient<ProjectDialog>();

        var serviceProvider = services.BuildServiceProvider();
        var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();

        await menuDialog.ShowMainMenuAsync();
    }
}

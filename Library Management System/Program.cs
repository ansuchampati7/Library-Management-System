using Library_Management_System.Domain.Infrastructure;
using Library_Management_System.Services;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.CommandTimeout(30)), ServiceLifetime.Transient);

        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBorrowService, BorrowService>();
        services.AddTransient<ILibraryApplicationService, LibraryApplicationService>();

        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var applicationService = scope.ServiceProvider.GetRequiredService<ILibraryApplicationService>();
    await applicationService.RunAsync();
}

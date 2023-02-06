using Competition.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Competition.Api.ComponentTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // var dbContextDescriptor = services.SingleOrDefault(
            //     d => d.ServiceType ==
            //          typeof(DbContextOptions<ApplicationDbContext>));
            //
            // services.Remove(dbContextDescriptor);
            //
            // var dbConnectionDescriptor = services.SingleOrDefault(
            //     d => d.ServiceType ==
            //          typeof(DbConnection));
            //
            // services.Remove(dbConnectionDescriptor);
            //
            // // Create open SqliteConnection so EF won't automatically close it.
            // services.AddSingleton<DbConnection>(container =>
            // {
            //     var connection = new SqliteConnection("DataSource=:memory:");
            //     connection.Open();
            //
            //     return connection;
            // });
            //
            // services.AddDbContext<ApplicationDbContext>((container, options) =>
            // {
            //     var connection = container.GetRequiredService<DbConnection>();
            //     options.UseSqlite(connection);
            // });

            services.AddTransient<GameService>();
        });

        builder.UseEnvironment("Development");
    }
}
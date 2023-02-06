using Competition.Api.Infrastructure;
using Competition.Api.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;
using Shop.Application.Core.Events;
using Shop.Application.Core.Providers;
using Shop.Application.Core.Queries;

namespace Competition.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // Data
        services.AddDbContext<CompetitionDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("competition");
        });
        
        // Commands
        services.Scan(scan => scan
            .FromAssemblyOf<ICommand>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblyOf<ICommand>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandValidator<>)))
            .AsSelfWithInterfaces()
            .WithSingletonLifetime()
        );

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
        
        // Queries
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IQuery<>))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
        
        services.AddSingleton<IQueryProcessor, QueryProcessor>();
        
        // Event
        services.Scan(scan => scan
            .FromAssemblyOf<IDomainEvent>()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();
        
        // External stuff
        services.AddSingleton<ISmsSender, PretendSmsProvider>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseRouting();
        app.UseEndpoints(e => { e.MapControllers(); });
    }
}
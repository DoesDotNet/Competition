using Shop.Application.Core.Commands;

namespace Shop.Application.Products.Commands;

public class CreateProductHandler : ICommandHandler<CreateProduct>
{
    public CreateProductHandler()
    {
        
    }
    
    public Task Handle(CreateProduct command)
    {
        return Task.CompletedTask;
    }
}
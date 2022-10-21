using Shop.Application.Core.Commands;

namespace Shop.Application.Products.Commands;

public record CreateProduct(Guid Id, string Name, string Description, decimal Price) : ICommand;
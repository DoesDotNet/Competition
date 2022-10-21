using Microsoft.AspNetCore.Mvc;
using Shop.Api.Products.Models;
using Shop.Application.Core.Commands;
using Shop.Application.Products.Commands;

namespace Shop.Api.Products;

[ApiController]
[Route("products/")]
public class ListProductsController : ControllerBase
{
    public ListProductsController()
    {
        
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> ListProducts()
    {
        
        
        return Ok();
    }
}

[ApiController]
[Route("products/test")]
public class CreateProductsontroller : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public CreateProductsontroller(ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateProduct(CreateProductModel model)
    {
        var command = new CreateProduct(model.Id, model.Name, model.Description, model.Price);
        await _dispatcher.Dispatch(command);
        
        return Ok();
    }
}
using System.Net;
using System.Net.Http.Json;
using Competition.Api.Games.GetGame;

namespace Competition.Api.ComponentTests;

public class GameTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public GameTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task CreateGame_Then_GetGame()
    {
        var client = _factory.CreateClient();

        var model = new
        {
            Id = Guid.NewGuid(),
            Name = "Test Game",
            Prize = "Monkey"
        };

        var createResponse = await client.PostAsJsonAsync("games", model); 
        createResponse.EnsureSuccessStatusCode();
        
        var getResponse = await client.GetAsync($"/games/{model.Id}");
        getResponse.EnsureSuccessStatusCode();

        GameModel? newGame = await getResponse.Content.ReadFromJsonAsync<GameModel>();
        
        Assert.NotNull(newGame);
        Assert.Equal(model.Id, newGame.Id);
        Assert.Equal(model.Name, newGame.Name);
        Assert.Equal(model.Prize, newGame.Prize);
    }
    
    [Fact]
    public async Task GetGame_ThatDoesntExists_ReturnsNotFound()
    {
        var client = _factory.CreateClient();
        
        var getResponse = await client.GetAsync($"/games/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
using System.Net;
using System.Net.Http.Json;
using Competition.Api.Games;
using Xunit;
using Xunit.Abstractions;

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

        GetGameResponse? newGame = await getResponse.Content.ReadFromJsonAsync<GetGameResponse>();
        
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

    [Fact]
    public async Task CreateGame_Start_AddEntry_ChooseWinner()
    {
        var client = _factory.CreateClient();
        
        var createGameRequest = new
        {
            Id = Guid.NewGuid(),
            Name = "Test Game",
            Prize = "Monkey"
        };

        var createResponse = await client.PostAsJsonAsync("games", createGameRequest);
        Assert.True(createResponse.IsSuccessStatusCode, "Error creating game");
        
        var startResponse = await client.PostAsync($"games/{createGameRequest.Id}/start", new StringContent("{}"));
        Assert.True(startResponse.IsSuccessStatusCode, "Error starting game");

        var addEntryRequest = new
        {
            createGameRequest.Id,
            Name = "Bob Smith",
            TelephoneNumber = "0123456789"
        };
        var addEntryResponse = await client.PostAsJsonAsync($"games/{createGameRequest.Id}/entries", addEntryRequest);
        Assert.True(addEntryResponse.IsSuccessStatusCode, "Error adding entry");
        
        var chooseWinnerResponse = await client.PostAsync($"games/{createGameRequest.Id}/winner", new StringContent("{}"));
        Assert.True(chooseWinnerResponse.IsSuccessStatusCode, "Error choosing winner");
    }

    [Fact]
    public async Task StartGame_GameDoesntExist_ReturnsNotFound()
    {
        var client = _factory.CreateClient();

        var gameId = Guid.NewGuid();
        var response = await client.PostAsync($"games/{gameId}/start", new StringContent("{}")); 
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
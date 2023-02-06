namespace Competition.Api.Games;

public class CreateGameModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Prize { get; set; }
}
namespace Competition.Api.Games;

public class GetGameResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Prize { get; set; }
    public string? Winner { get; set; }
}
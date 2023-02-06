using Shop.Application.Games.Events;

namespace Shop.Application.Domain;

public class Game : Entity
{
    public Guid Id { get; private set;}

    public string Name { get; private set;}

    public bool IsLive { get; private set; }

    public bool IsClosed { get; private set; }

    public string Prize { get; private set;}

    public Entry Winner { get; private set; }
    
    public DateTime TimeStarded { get; private set; }
    public DateTime TimeEnded { get; private set; }

    private List<Entry> _entries;

    public IReadOnlyCollection<Entry> Entries => _entries.AsReadOnly();

    private Game() { }

    private Game(Guid id, string name, string prize)
    {
        Id = id;
        Name = name;
        Prize = prize;
    }

    public static Game Create(Guid id, string name, string prize)
    {
        return new Game(id, name, prize);
    }

    public void AddEntry(Entry entry)
    {
        if (!IsLive || IsClosed)
        {
            return;
        }

        _entries.Add(entry);
    }

    public void Start()
    {
        IsLive = true;
        TimeStarded = DateTime.Now;
    }

    public void ChooseWinner()
    {
        if (Winner != null)
            throw new Exception("Cannot choose winner, winner has already been chosen.");
        
        IsClosed = true;
        IsLive = false;
        TimeEnded = DateTime.Now;
        
        if (_entries.Any())
        {
            var random = new Random();
            int index = random.Next(_entries.Count);

            Winner = _entries[index];
            
            AddEvent(new WinnerChosen(Id, Winner.Name, Winner.TelephoneNumber));
        }
    }
}
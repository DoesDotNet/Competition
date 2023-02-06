namespace Shop.Application.Domain;

public class Entry : ValueObject
{
    public string Name { get; init; }
    public string TelephoneNumber { get; init; }

    public Entry()
    {
            
    }

    public Entry(string name, string telephoneNumber)
    {
        Name = name;
        TelephoneNumber = telephoneNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return TelephoneNumber;
    }
}
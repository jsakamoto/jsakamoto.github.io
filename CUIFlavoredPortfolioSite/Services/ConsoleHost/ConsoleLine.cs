namespace CUIFlavoredPortfolioSite.Services.ConsoleHost;

public class ConsoleLine
{
    public int Id { get; }

    private readonly List<ConsoleFragment> _Fragments = new List<ConsoleFragment>();

    public IEnumerable<ConsoleFragment> Fragments => this._Fragments;

    public ConsoleLine(int id)
    {
        this.Id = id;
    }

    public void AddFragment(ConsoleFragment fragment)
    {
        this._Fragments.Add(fragment);
    }

    public void AddFragments(IEnumerable<ConsoleFragment> fragments)
    {
        this._Fragments.AddRange(fragments);
    }
}

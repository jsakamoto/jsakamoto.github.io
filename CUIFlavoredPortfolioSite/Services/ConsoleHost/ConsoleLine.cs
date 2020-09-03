using System.Collections.Generic;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleLine
    {
        public int Id { get; }

        private readonly List<ConsoleFragment> _Fragments = new List<ConsoleFragment>();

        public IEnumerable<ConsoleFragment> Fragments => _Fragments;

        public ConsoleLine(int id)
        {
            Id = id;
        }

        public void AddFragments(IEnumerable<ConsoleFragment> fragments)
        {
            _Fragments.AddRange(fragments);
        }
    }
}

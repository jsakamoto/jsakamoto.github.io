using CUIFlavoredPortfolioSite.Services;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Xunit;

namespace CUIFlavoredPortfolioSite.Test;

public class CommandCompletionTest
{
    private class TestCommand : ICommand
    {
        public IEnumerable<string> Names { get; }
        public string Description => throw new NotImplementedException();
        public void Invoke(IConsoleHost consoleHost, string[] args) => throw new NotImplementedException();
        public TestCommand(params string[] names) { this.Names = names; }
    }

    private readonly IEnumerable<ICommand> _TestCommands = new[] {
            new TestCommand("help"),
            new TestCommand("figgle", "figlet"),
            new TestCommand("cls", "clear"),
            new TestCommand("banner"),
        };

    [Fact]
    public void Completion_NoMatch_Test()
    {
        var completion = new CommandCompletion(this._TestCommands);
        completion.Completion("foo").Is("foo");
        completion.Completion("foo").Is("foo");
    }

    [Fact]
    public void Completion_FromEmpty_Test()
    {
        var completion = new CommandCompletion(this._TestCommands);
        completion.Completion("").Is("banner");
        completion.Completion("banner").Is("clear");
        completion.Completion("clear").Is("cls");
        completion.Completion("cls").Is("figgle");
        completion.Completion("figgle").Is("figlet");
        completion.Completion("figlet").Is("help");
        completion.Completion("help").Is("banner");
        completion.Completion("banner").Is("clear");
    }

    [Fact]
    public void Completion_FromEmpty_to_NoMatch_Test()
    {
        var completion = new CommandCompletion(this._TestCommands);
        completion.Completion("").Is("banner");
        completion.Completion("banner").Is("clear");

        completion.Completion("bar").Is("bar");
    }

    [Fact]
    public void Completion_FromEmpty_to_AnotherMatch_Test()
    {
        var completion = new CommandCompletion(this._TestCommands);
        completion.Completion("").Is("banner");
        completion.Completion("banner").Is("clear");

        completion.Completion("fig").Is("figgle");
        completion.Completion("figgle").Is("figlet");
        completion.Completion("figlet").Is("figgle");
        completion.Completion("figgle").Is("figlet");

        completion.Completion("he").Is("help");
        completion.Completion("help").Is("help");

        completion.Completion("").Is("banner");
        completion.Completion("banner").Is("clear");
    }

    [Fact]
    public void Completion_Match_to_AnotherMatch_Test()
    {
        var completion = new CommandCompletion(this._TestCommands);
        completion.Completion("c").Is("clear");
        completion.Completion("clear").Is("cls");
        completion.Completion("cls").Is("clear");

        completion.Completion("he").Is("help");
        completion.Completion("help").Is("help");

        completion.Completion("").Is("banner");
        completion.Completion("banner").Is("clear");
    }
}

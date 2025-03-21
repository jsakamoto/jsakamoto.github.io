using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using CUIFlavoredPortfolioSite.Test.Internals;
using NUnit.Framework;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Test;

public class ConsoleHostServiceTest
{
    [Test]
    public void Write_Test()
    {
        var host = new ConsoleHostService();
        host.Write("Foo");
        host.Lines.Dump().Is("t:Foo");

        host.Write("Bar");
        host.Lines.Dump().Is("t:Foo|t:Bar");

        host.Write("Fizz\r\nBuzz");
        host.Lines.Dump().Is(
            "t:Foo|t:Bar|t:Fizz",
            "t:Buzz");

        host.Write("FizzBuzz");
        host.Lines.Dump(Dump.ForeColor).Is(
            "t:Foo,f:#cccccc|t:Bar,f:#cccccc|t:Fizz,f:#cccccc",
            "t:Buzz,f:#cccccc|t:FizzBuzz,f:#cccccc");
    }

    [Test]
    public void WriteLine_Test()
    {
        var host = new ConsoleHostService();
        host.Write("Foo");
        host.Lines.Dump().Is("t:Foo");

        host.WriteLine("Bar");
        host.Lines.Dump().Is("t:Foo|t:Bar");

        host.WriteLine("Fizz");
        host.WriteLine();
        host.Lines.Dump().Is(
            "t:Foo|t:Bar",
            "t:Fizz",
            "t:");

        host.Write("Buzz");
        host.Lines.Dump().Is(
            "t:Foo|t:Bar",
            "t:Fizz",
            "t:",
            "t:Buzz");

        host.WriteLine("Lorem\nIpsum");
        host.Lines.Dump(Dump.ForeColor).Is(
            "t:Foo,f:#cccccc|t:Bar,f:#cccccc",
            "t:Fizz,f:#cccccc",
            "t:,f:#cccccc",
            "t:Buzz,f:#cccccc|t:Lorem,f:#cccccc",
            "t:Ipsum,f:#cccccc");
    }

    [Test]
    public void Clear_Test()
    {
        var host = new ConsoleHostService();
        host.WriteLine("Foo").WriteLine("Bar");
        host.Lines.Dump().Is("t:Foo", "t:Bar");

        host.Clear();
        host.Lines.Count().Is(0);

        host.Write("Fizz");
        host.Lines.Dump().Is("t:Fizz");
    }

    [Test]
    public void CorlorizeToFragments_Test()
    {
        var host = new ConsoleHostService();
        host.WriteLine($"{Red("Lorem")} ipsum dolor sit");
        host.WriteLine($"Lorem {Green("ipsum")}{Yellow(" dolor")} sit");
        host.WriteLine($"Lorem ipsum dolor {Blue("sit")}");
        host.WriteLine($"Lorem ipsum dolor sit");

        host.Lines.Dump(Dump.ForeColor).Is(
            "t:Lorem,f:#e74856|t: ipsum dolor sit,f:#cccccc",
            "t:Lorem ,f:#cccccc|t:ipsum,f:#13c60d|t: dolor,f:#f9f1a5|t: sit,f:#cccccc",
            "t:Lorem ipsum dolor ,f:#cccccc|t:sit,f:#3b78ff",
            "t:Lorem ipsum dolor sit,f:#cccccc");
    }

    [Test]
    public void HyperLinkFragments_Test()
    {
        var host = new ConsoleHostService();
        host.WriteLine($"This is [example](https://example.com) site.");

        host.Lines.Dump(Dump.Link).Is(
            "t:This is ,l:|t:example,l:https://example.com|t: site.,l:");
    }
}

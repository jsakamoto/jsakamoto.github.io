using System;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using CUIFlavoredPortfolioSite.Test.Internals;
using Xunit;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Test
{
    public class ConsoleHostServiceTest
    {
        [Fact]
        public void Write_Test()
        {
            var host = new ConsoleHostService();
            host.Write("Foo");
            host.Lines.Dump().Is("t:Foo");

            host.Write("Bar");
            host.Lines.Dump(Dump.ForeColor)
                .Is("t:Foo,f:#cccccc|t:Bar,f:#cccccc");
        }

        [Fact]
        public void WriteLine_Test()
        {
            var host = new ConsoleHostService();
            host.Write("Foo");
            host.Lines.Dump().Is("t:Foo");

            host.WriteLine("Bar");
            host.Lines.Dump().Is("t:Foo|t:Bar");

            host.WriteLine("Fizz");
            host.Lines.Dump().Is(
                "t:Foo|t:Bar",
                "t:Fizz");

            host.Write("Buzz");
            host.Lines.Dump(Dump.ForeColor).Is(
                "t:Foo,f:#cccccc|t:Bar,f:#cccccc",
                "t:Fizz,f:#cccccc",
                "t:Buzz,f:#cccccc");
        }

        [Fact]
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
    }
}

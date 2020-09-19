using System;
using CUIFlavoredPortfolioSite.Services;
using Xunit;

namespace CUIFlavoredPortfolioSite.Test
{
    public class CommandHistoryTest
    {
        [Fact]
        public void PrevNext_on_EmptyBuff_Test()
        {
            var commandHistory = new CommandHistory();
            commandHistory.TryGetNext(out var _).Is(false);
            commandHistory.TryGetPrevious(out var _).Is(false);
        }

        [Fact]
        public void Push_EmptyCommand_Test()
        {
            var commandHistory = new CommandHistory();
            commandHistory.Push("");

            commandHistory.TryGetPrevious(out var _).Is(false);
            commandHistory.Push(" ");
            commandHistory.Push("\t");
            commandHistory.TryGetPrevious(out var _).Is(false);
            commandHistory.TryGetNext(out var _).Is(false);
        }

        [Fact]
        public void PushNextPrev_Combo_Test()
        {
            var commandHistory = new CommandHistory();
            commandHistory.Push("foo -a");
            commandHistory.Push("foo -b");

            commandHistory.TryGetNext(out var _).Is(false);
            commandHistory.TryGetPrevious(out var c1).Is(true);
            c1.Is("foo -b");
        }

        [Fact]
        public void Push_and_PrevNext_Test()
        {
            var commandHistory = new CommandHistory();
            commandHistory.Push("foo -a");
            commandHistory.Push("foo -b");
            commandHistory.Push("bar");

            commandHistory.TryGetPrevious(out var c1).Is(true);
            c1.Is("bar");
            commandHistory.TryGetPrevious(out var c2).Is(true);
            c2.Is("foo -b");
            commandHistory.TryGetNext(out var c3).Is(true);
            c3.Is("bar");
            commandHistory.TryGetNext(out var _).Is(false);

            commandHistory.TryGetPrevious(out var c4).Is(true);
            c4.Is("foo -b");
            commandHistory.TryGetPrevious(out var c5).Is(true);
            c5.Is("foo -a");
            commandHistory.TryGetPrevious(out var _).Is(false);

            commandHistory.Push("foo -a");

            commandHistory.TryGetPrevious(out var c6).Is(true);
            c6.Is("foo -a");
            commandHistory.TryGetPrevious(out var _).Is(false);
            commandHistory.TryGetNext(out var c7).Is(true);
            c7.Is("foo -b");

            commandHistory.Push("foo -b");

            commandHistory.TryGetNext(out var c8).Is(true);
            c8.Is("bar");
            commandHistory.TryGetPrevious(out var c9).Is(true);
            c9.Is("foo -b");

            commandHistory.Push("fizz");

            commandHistory.TryGetPrevious(out var c10).Is(true);
            c10.Is("fizz");
            commandHistory.TryGetPrevious(out var c11).Is(true);
            c11.Is("bar");
            commandHistory.TryGetPrevious(out var c12).Is(true);
            c12.Is("foo -b");
        }

        [Fact]
        public void BufferOverflow_Test()
        {
            var commandHistory = new CommandHistory { HistoryBuffSize = 3 };
            commandHistory.Push("foo -a"); // <- overflow!
            commandHistory.Push("foo -b");
            commandHistory.Push("fizz");
            commandHistory.Push("buzz");

            commandHistory.TryGetPrevious(out var c1).Is(true);
            c1.Is("buzz");
            commandHistory.TryGetPrevious(out var c2).Is(true);
            c2.Is("fizz");
            commandHistory.TryGetPrevious(out var c3).Is(true);
            c3.Is("foo -b");
            commandHistory.TryGetPrevious(out var _).Is(false);
        }
    }
}

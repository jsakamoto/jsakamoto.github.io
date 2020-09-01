﻿using System;
using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Test.Internals
{
    [Flags]
    internal enum Dump
    {
        Default = 0b_0000,
        ForeColor = 0b_0001,
    }

    internal static class ConsoleHostExtensions
    {
        public static IEnumerable<string> Dump(this IEnumerable<ConsoleLine> lines, Dump flags = Internals.Dump.Default)
        {
            return lines
                .Select(l => string.Join('|', l.Fragments.Dump(flags)));
        }

        public static IEnumerable<string> Dump(this IEnumerable<ConsoleFragment> fragments, Dump flags = Internals.Dump.Default)
        {
            return fragments.Select(f =>
                $"t:{f.Text}" +
                (flags.HasFlag(Internals.Dump.ForeColor) ? $",f:#{f.ForeColor.R:x2}{f.ForeColor.G:x2}{f.ForeColor.B:x2}" : "")
            );
        }
    }
}

namespace CUIFlavoredPortfolioSite.Commands.Helpers;

public static class CommandOptionsParser
{
    public static bool TryParse<TOptions>(ref string[] args, out TOptions options, out string errorMessage) where TOptions : new()
    {
        options = new();
        errorMessage = "";
        var args2 = new List<string>();

        var props = typeof(TOptions).GetProperties();
        foreach (var arg in args)
        {
            if (!arg.StartsWith("-"))
            {
                args2.Add(arg);
                continue;
            }

            foreach (var optionName in arg.ToCharArray().Skip(1))
            {
                var prop = props.FirstOrDefault(prop => prop.Name.ToLower().StartsWith(optionName));
                if (prop == null)
                {
                    errorMessage = $"invalid option -- '{optionName}'";
                    return false;
                }
                prop.SetValue(options, true);
            }
        }

        args = args2.ToArray();
        return true;
    }
}

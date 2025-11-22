namespace CUIFlavoredPortfolioSite.Services;

public class PathUtility
{
    public string ReplaceUserHomePath(string path)
    {
        var homeDir = Environment.GetEnvironmentVariable("HOME");
        if (homeDir is not null)
        {
            if (path == homeDir || path.StartsWith(homeDir + Path.DirectorySeparatorChar) || path.StartsWith(homeDir + Path.AltDirectorySeparatorChar))
            {
                path = string.Concat("~", path.AsSpan(homeDir.Length));
            }
        }
        return path;
    }

    public string RevertUserHomePath(string path)
    {
        return path.Replace("~", Environment.GetEnvironmentVariable("HOME"));
    }

    public string GetCurrentDirectoryDisplayText() => this.ReplaceUserHomePath(Environment.CurrentDirectory);
}

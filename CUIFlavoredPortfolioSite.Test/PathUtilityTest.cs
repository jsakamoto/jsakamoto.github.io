using CUIFlavoredPortfolioSite.Services;
using Xunit;

namespace CUIFlavoredPortfolioSite.Test;

public class PathUtilityTest
{
    [Fact]
    public void ReplaceUserHomePath_Test()
    {
        // Given
        Environment.SetEnvironmentVariable("HOME", "/home/web_user1");
        var pathUtility = new PathUtility();

        // When & Then ...
        pathUtility.ReplaceUserHomePath("/home/foo").Is("/home/foo");

        pathUtility.ReplaceUserHomePath("/home/web_user1").Is("~");
        pathUtility.ReplaceUserHomePath("/home/web_user1/").Is("~/");
        pathUtility.ReplaceUserHomePath("/home/web_user1/foo").Is("~/foo");
        pathUtility.ReplaceUserHomePath("/home/web_user1/fizz/buzz").Is("~/fizz/buzz");

        pathUtility.ReplaceUserHomePath("/home/web_user2").Is("/home/web_user2");
        pathUtility.ReplaceUserHomePath("/home/web_user12").Is("/home/web_user12");
    }

    [Fact]
    public void RevertUserHomePath_Test()
    {
        // Given
        Environment.SetEnvironmentVariable("HOME", "/home/web_user1");
        var pathUtility = new PathUtility();
     
        // When & Then ...
        pathUtility.RevertUserHomePath("/home/foo").Is("/home/foo");
        pathUtility.RevertUserHomePath("~").Is("/home/web_user1");
        pathUtility.RevertUserHomePath("~/").Is("/home/web_user1/");
        pathUtility.RevertUserHomePath("~/foo").Is("/home/web_user1/foo");
        pathUtility.RevertUserHomePath("~/fizz/buzz").Is("/home/web_user1/fizz/buzz");
        pathUtility.RevertUserHomePath("/home/web_user2").Is("/home/web_user2");
        pathUtility.RevertUserHomePath("/home/web_user12").Is("/home/web_user12");
    }
}

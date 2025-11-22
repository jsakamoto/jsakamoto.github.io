using System.Diagnostics.CodeAnalysis;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost;

public class ANSIColorToRGB
{
    private static readonly IReadOnlyDictionary<string, string> _Mappings = new Dictionary<string, string>
    {
        ["\x1b[0m"] = "#cccccc", // (Default Foreground Color)

        ["\x1b[90m"] = "#767676", // DarkGray   
        ["\x1b[91m"] = "#e74856", // Red        
        ["\x1b[92m"] = "#13c60d", // Green      
        ["\x1b[93m"] = "#f9f1a5", // Yellow     
        ["\x1b[94m"] = "#3b78ff", // Blue       
        ["\x1b[95m"] = "#b4009e", // Magenta    
        ["\x1b[96m"] = "#61d6d6", // Cyan       
        ["\x1b[97m"] = "#f2f2f2", // White      

        ["\x1b[30m"] = "#0d0d0d", // Black      
        ["\x1b[31m"] = "#c50f1f", // DarkRed    
        ["\x1b[32m"] = "#13a10e", // DarkGreen  
        ["\x1b[33m"] = "#c19c00", // DarkYellow 
        ["\x1b[34m"] = "#0037da", // DarkBlue   
        ["\x1b[35m"] = "#881798", // DarkMagenta
        ["\x1b[36m"] = "#3a96dd", // DarkCyan   
        ["\x1b[37m"] = "#cccccc", // Gray       
    };

    public static bool TryGetRGB(string ansiCode, [NotNullWhen(true)] out string? rgb)
    {
        return _Mappings.TryGetValue(ansiCode, out rgb);
    }
}

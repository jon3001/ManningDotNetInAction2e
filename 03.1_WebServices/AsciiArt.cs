using Figgle;
using System.Reflection;
using System.Linq;

namespace AsciiArtSvc;
public static class AsciiArt
{
    public static bool Write(string text, out string? asciiText, string? fontName = null)
    {
        FiggleFont? font = null;
        if (!string.IsNullOrWhiteSpace(fontName))
        {
            font = typeof(FiggleFonts)
                .GetProperty(fontName, BindingFlags.Static | BindingFlags.Public)?
                .GetValue(null)
                    as FiggleFont;
        }
        else
        {
            font = FiggleFonts.Standard;
        }

        if (font == null)
        {
            asciiText = null;
            return false;
        }

        asciiText = font.Render(text);
        return true;
    }

    public static Lazy<IEnumerable<(string Name, FiggleFont Font)>> AllFonts =
        new(() => 
            typeof(FiggleFonts)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(p => (p.Name, Font: (p.GetValue(null) as FiggleFont)!))
        );
}

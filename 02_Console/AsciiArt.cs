using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace HelloDotNet;

public static class AsciiArt
{
    public static void Write(Options o)
    {
        FiggleFont? font = null;
        if (!string.IsNullOrWhiteSpace(o.Font))
        {
            font = typeof(FiggleFonts)
                .GetProperty(o.Font, BindingFlags.Static | BindingFlags.Public)?
                .GetValue(null)
                    as FiggleFont;

            if (font == null)
            {
                WriteLine($"Count not find font '{o.Font}'");
            }
        }

        font ??= FiggleFonts.Standard;

        if (o?.Text != null)
        {

            WriteLine(font.Render(o.Text));
            WriteLine("Brought to you by " + typeof(AsciiArt).FullName);
        }
    }
}

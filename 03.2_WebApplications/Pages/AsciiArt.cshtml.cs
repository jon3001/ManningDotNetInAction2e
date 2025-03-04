using Figgle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03._2_WebApplications.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class AsciiArtModel : PageModel
{
    public string? RenderedText { get; set; }

    public bool ShowRenderedText => !string.IsNullOrEmpty(RenderedText);

    public AsciiArtModel()
    {
        
    }

    public void OnPost(string text)
    {
        RenderedText = FiggleFonts.Standard.Render(text ?? "Hello!");
    }
}

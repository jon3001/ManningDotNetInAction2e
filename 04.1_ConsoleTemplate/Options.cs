using CommandLine;

namespace ConsoleTemplate;
public record Options
{
    [Value(0, Required = true, HelpText = "Some Text")]
    public string? Text { get; init; }
}

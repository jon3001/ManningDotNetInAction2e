using ConsoleTemplate;
using CommandLine;
using CommandLine.Text;

var results = Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(option =>
    {
        WriteLine(option.Text);
    });

results.WithNotParsed(_ => WriteLine(HelpText.RenderUsageText(results)));

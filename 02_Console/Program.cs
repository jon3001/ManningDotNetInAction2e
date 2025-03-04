using HelloDotNet;
using CommandLine;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(AsciiArt.Write)
    .WithNotParsed(_ => WriteLine("Usage: HelloDotNet <text> --font Big"));


﻿using CommandLine;
using CommandLine.Text;
using System.Text.Json;
using System.Text;
using GetJokes;

var results = Parser.Default.ParseArguments<Options>(args)
  .WithParsed<Options>(options => {
      var inFile = new FileInfo(options.InputFile!);
      if (!inFile.Exists)
      {
          Console.Error.WriteLine($"{inFile.FullName} does not exist");
          return;
      }

      using var inStream = inFile.OpenRead();

      FileInfo? outFile = null;
      if (options.OutputFile != null)
      {
          outFile = new FileInfo(options.OutputFile);
          if (outFile.Exists)
          {
              outFile.Delete();
          }
      }

      using var outStream = outFile != null
        ? outFile.OpenWrite()
        : Console.OpenStandardOutput();

      FindWithSerialization(inStream, outStream, options.Category);
  });


static void FindWithSerialization(Stream inStream, Stream outStream, string category)
{
    var serialOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    var jokes = JsonSerializer.Deserialize<Joke[]>(inStream, serialOptions);

    JsonSerializer.Serialize(
      outStream,
      jokes?
        .Where(j => string.Equals(category, j.Type,StringComparison.OrdinalIgnoreCase))
        .Select(j => new { setup = j.Setup, punchline = j.Punchline})
        .ToArray(),
      serialOptions);
}

//static void FindWithJsonDom(Stream inStream, Stream outStream, string category)
//{
//    var writerOptions = new JsonWriterOptions { Indented = true };
//    using var writer = new Utf8JsonWriter(outStream, writerOptions);
//    writer.WriteStartArray();

//    using var jsonDoc = JsonDocument.Parse(inStream);

//    foreach (var jokeElement in jsonDoc.RootElement.EnumerateArray())
//    {
//        string? type = jokeElement.GetProperty("type").GetString();
//        if (string.Equals(category, type, StringComparison.OrdinalIgnoreCase))
//        {
//            string? setup = jokeElement.GetProperty("setup").GetString();
//            string? punchline = jokeElement.GetProperty("punchline").GetString();
//            writer.WriteStartObject();
//            writer.WriteString("setup", setup);
//            writer.WriteString("punchline", punchline);
//            writer.WriteEndObject();
//        }
//    }

//    writer.WriteEndArray();
//}

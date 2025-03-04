using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingLargeStrings;

public static class Utils
{
    public static void OutputStreamToConsole(Stream stream)
    {
        var reader = new StreamReader(stream);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
    }
}
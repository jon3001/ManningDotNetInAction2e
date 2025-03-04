using WritingLargeStrings;

Example01();
Example02();
Example03();
Example04();
Example05();

static void Example01() /* Normal strings */
{
    using MemoryStream stream = new();
    StreamWriter writer = new(stream);

    writer.WriteLine("<?xml version=\"1.0\"?>");
    writer.WriteLine("<books>");
    writer.WriteLine("\t<book title=\".NET in Action\"/>");
    writer.WriteLine("</books>");

    writer.Flush();
    stream.Seek(0, SeekOrigin.Begin);

    Utils.OutputStreamToConsole(stream);
}

static void Example02() /* Verbatim strings */
{
    using MemoryStream stream = new();
    StreamWriter writer = new(stream);

    writer.Write(
@"<?xml version=""1.0""?>
<books>
  <book title="".NET in Action""/>
</books>
");

    writer.Flush();
    stream.Seek(0, SeekOrigin.Begin);

    Utils.OutputStreamToConsole(stream);
}


static void Example03() /* Verbatim interpolated string */
{
    string bookTitle = "The Hobbit";

    using MemoryStream stream = new();
    StreamWriter writer = new(stream);

    writer.WriteLine(@$"<?xml version=""1.0""?>
<books>
  <book title=""{bookTitle}""/>
</books>
");

    writer.Flush();
    stream.Seek(0, SeekOrigin.Begin);

    Utils.OutputStreamToConsole(stream);
}

static void Example04() /* JSON in a verbatim interpolated string literal */
{
    var userId = 1001;
    var resourceId = Guid.NewGuid();

    using MemoryStream stream = new();
    StreamWriter writer = new(stream);

    writer.WriteLine( /*lang=json,strict*/
@$"{{
  ""RequestParams"": {{
    ""UserId"": ""{userId}"",
    ""ResourceId"": ""{resourceId}""
  }}
}}");


    writer.Flush();
    stream.Seek(0, SeekOrigin.Begin);

    Utils.OutputStreamToConsole(stream);
}

static void Example05() /* JSON in a verbatim interpolated string literal */
{
    var jsonString = /*lang=json,strict*/ """
    {
      "RequestParams": {
        "UserId": "myuserId",
        "ResourceId": "myResourceId"
      }
    }
    """;

    Console.Write(jsonString);
}


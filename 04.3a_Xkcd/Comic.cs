using System.Text.Json;
using System.Text.Json.Serialization;

namespace XkcdSearch;

public record Comic
{
    [JsonPropertyName("num")]
    public int Number { get; init; }
    [JsonPropertyName("safe_title")]
    public string? Title { get; init; }
    [JsonPropertyName("month")]
    public string? Month { get; init; }
    [JsonPropertyName("day")]
    public string? Day { get; init; }
    [JsonPropertyName("year")]
    public string? Year { get; init; }
    [JsonIgnore]
    public DateOnly Date =>
      DateOnly.Parse($"{Year}-{Month}-{Day}");

    private static HttpClient client = new HttpClient()
    {
            BaseAddress = new Uri("https://xkcd.com")
    };

    public static Comic? GetComic(int number)
    {
        try
        {
            var path = GetPath(number);
            var stream = client.GetStreamAsync(path).Result;
            return JsonSerializer.Deserialize<Comic>(stream);
        }
        catch (AggregateException e) when (e.InnerException is HttpRequestException)
        {
            return null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public static async Task<Comic?> GetComicAsync(int number, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }

        try
        {
            var path = GetPath(number);
            var stream = await client.GetStreamAsync(path, cancellationToken);
            var comic = await JsonSerializer.DeserializeAsync<Comic>(stream, cancellationToken: cancellationToken);
            return comic;
        }
        catch (Exception ex) when (
          (ex is AggregateException && ex.InnerException is HttpRequestException) || 
           ex is HttpRequestException || 
           ex is TaskCanceledException
          )
        {
            return null;
        }
    }

    private static string GetPath(int number)
    {
        return number == 0
            ? "info.0.json"
            : $"{number}/info.0.json";
    }
}

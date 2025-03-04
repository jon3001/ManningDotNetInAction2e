namespace ManningBooks;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public List<Rating> Ratings { get; } = new();
}

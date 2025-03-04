using ManningBooks;
using Microsoft.EntityFrameworkCore;

BookWithRatingsDemo();
BookDemo();

static void BookDemo()
{
    using var dbContext = new CatalogContext();
    dbContext.Add(new Book { Title = ".NET in Action" });
    dbContext.Add(new Book { Title = "API Design Patterns" });
    dbContext.Add(new Book { Title = "Grokking Simplicity" });
    dbContext.Add(new Book { Title = "The Programmer's Brain" });
    dbContext.SaveChanges();

    var books = dbContext.Books.Include(b => b.Ratings);

    foreach (var book in books)
    {
        Console.WriteLine($"\"{book.Title}\" has id {book.Id}");
        book.Ratings.ForEach(r => Console.WriteLine($"\t{r.Stars} stars: {r.Comment ?? "-blanks-"}"));
    }
}

static void BookWithRatingsDemo()
{
    var dbContext = new CatalogContext();

    var efBook = new Book { Title = "EF Core in Action" };
    efBook.Ratings.Add(new Rating { Comment = "Great!" });
    efBook.Ratings.Add(new Rating { Stars = 4 });
    dbContext.Add(efBook);
    dbContext.SaveChanges();

    //var efRatings = (from b in dbContext.Books
    //                 where b.Title == "EF Core in Action"
    //                 select b.Ratings)
    //                .FirstOrDefault();

    var efRatings = dbContext.Books
        .Where(b => b.Title == "EF Core in Action")
        .Select(b => b.Ratings)
        .FirstOrDefault();

    efRatings?
        .ForEach(r => Console.WriteLine($"{r.Stars} stars: {r.Comment ?? "-blank-"}"));
}


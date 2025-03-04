using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManningBooksApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController(CatalogContext dbContext) : ControllerBase
{
    [HttpGet]
    public IAsyncEnumerable<Book> GetBooks(string? titleFilter = null)
    {
        IQueryable<Book> query = dbContext.Books
            .Include(b => b.Ratings)
            .AsNoTracking();

        if (titleFilter != null)
        {
            query = query.Where(b => b.Title.ToLower().Contains(titleFilter.ToLower()));
        }

        return query.AsAsyncEnumerable();
    }

    [HttpGet("{id}")]
    public Task<Book?> GetBook(int id)
    {
        return dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    [HttpPost]
    public async Task<Book> CreateBookAsync(BookCreateCommand command, CancellationToken cancellationToken)
    {
        var book = new Book(
          command.Title,
          command.Description
        );

        var entity = dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBookAsync(int id, BookUpdateCommand command,CancellationToken cancellationToken)
    {
        var book = await dbContext.FindAsync<Book>(new object?[] { id }, cancellationToken);
        if (book == null)
        {
            return NotFound();
        }

        if (command.Title != null)
        {
            book.Title = command.Title;
        }

        if (command.Description != null)
        {
            book.Description = command.Description;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBookAsync(int id, CancellationToken cancellationToken)
    {
        //var book = await dbContext.FindAsync<Book>(id, cancellationToken);

        var book = await dbContext.Books
            .Include(b => b.Ratings)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (book == null)
        {
            return NotFound();
        }

        dbContext.Remove(book);
        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    public record BookCreateCommand(
        string Title, 
        string? Description
    ) { }

    public record BookUpdateCommand(
        string? Title, 
        string? Description
    ) { }
}

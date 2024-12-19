namespace LibraryApp.Application.Filters;

public class BookFilters
{
    public string? Title { get; set; } = string.Empty;
    public string? Genre { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;

    public BookFilters()
    {
    }

    public BookFilters(string? title, string? genre, string? author)
    {
        Title = title;
        Genre = genre;
        Author = author;
    }
}
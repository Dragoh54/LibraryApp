namespace LibraryApp.DataAccess.Dto;

public class CreateBookDto
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;

    public Guid AuthorId { get; set; }
    
    public CreateBookDto() {}

    public CreateBookDto(string isbn, string title, string description, string genre, Guid authorId)
    {
        ISBN = isbn;
        Title = title;
        Description = description;
        Genre = genre;
        AuthorId = authorId;
    }
}
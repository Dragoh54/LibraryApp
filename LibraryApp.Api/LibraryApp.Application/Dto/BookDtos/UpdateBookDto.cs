namespace LibraryApp.DataAccess.Dto;

public record UpdateBookDto
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;

    public Guid AuthorId { get; set; }
    
    public UpdateBookDto() {}

    public UpdateBookDto(Guid id, string isbn, string title, string description, string genre, Guid authorId)
    {
        Id = id;
        ISBN = isbn;
        Title = title;
        Description = description;
        Genre = genre;
        AuthorId = authorId;
    }
    
}
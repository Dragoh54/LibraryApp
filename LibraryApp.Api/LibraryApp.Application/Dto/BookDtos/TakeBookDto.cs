namespace LibraryApp.DataAccess.Dto;

public record TakeBookDto
{
    public Guid Id { get; set; }
    public string UserClaimId { get; set; } = string.Empty;
}
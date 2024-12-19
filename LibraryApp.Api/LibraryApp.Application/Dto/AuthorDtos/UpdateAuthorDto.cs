namespace LibraryApp.Application.Dto;

public record UpdateAuthorDto
{
    public Guid Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public UpdateAuthorDto() {}

    public UpdateAuthorDto(Guid id, string surname, string country, DateTime birthDate)
    {
        Id = id;
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
    }
}
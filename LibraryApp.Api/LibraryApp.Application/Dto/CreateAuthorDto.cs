namespace LibraryApp.DataAccess.Dto;

public record CreateAuthorDto
{
    public string Surname { get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public CreateAuthorDto() {}

    public CreateAuthorDto(string surname, string country, DateTime birthDate)
    {
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
    }
}
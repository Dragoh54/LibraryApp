namespace LibraryApp.Application.Filters;

public class AuthorFilters
{
    public string? Surname { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? DateOfBirth { get; set; } = string.Empty;

    public AuthorFilters()
    {
    }

    public AuthorFilters(string? surname, string? country, string? dateOfBirth)
    {
        Surname = surname;
        Country = country;
        DateOfBirth = dateOfBirth;
    }
}
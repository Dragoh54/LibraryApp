namespace LibraryApp.Application.Dto;

public record RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
}
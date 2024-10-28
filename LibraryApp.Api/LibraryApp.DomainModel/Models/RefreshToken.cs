namespace LibraryApp.Entities.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; } = false;
    public DateTime WhenUsed { get; set; } = DateTime.MinValue;

    public RefreshToken() {}

    public RefreshToken(Guid id, string token, Guid userId, DateTime expiryDate)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiryDate = expiryDate;

    }
}
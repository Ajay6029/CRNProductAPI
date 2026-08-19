namespace Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public DateTime ExpiresOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? RevokedOn { get; set; }

    public bool IsRevoked => RevokedOn.HasValue;

    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
}
using System.ComponentModel.DataAnnotations;

namespace NvSystem.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new Exception("Email address cannot be null or empty");
        
        Email = email;
    }
    
}
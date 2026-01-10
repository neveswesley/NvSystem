using NvSystem.Domain.Enums;

namespace NvSystem.Communications.Responses;

public class ResponseRegisterUserJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}
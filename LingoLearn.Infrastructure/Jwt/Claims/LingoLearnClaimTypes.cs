using System.Security.Claims;

namespace LingoLearn.Infrastructure.Jwt.Claims;

public class LingoLearnClaimTypes
{
    public const string Role = ClaimTypes.Role;
    public const string NameIdentifier = ClaimTypes.NameIdentifier;
    public const string UserData = ClaimTypes.UserData;
    public const string Permissions = "permissions";
    public const string UserType = "user-type";
    public const string TransferredProps = "transferred-prop";
    public const string GenerationStamp = "generation-stamp";
    public const string GenerateDate = "generate-date";
    public const string Token = "Authorization";
    public const string Owner = "owner";
}
using Domain.Entities.Security;
using Domain.Enum;
using EasyRefreshToken.Result;
using Microsoft.AspNetCore.Identity;
using Neptunee.EntityFrameworkCore.Repository;

namespace Domain.Repositories;

public interface IUserRepository : INeptuneeRepository
{
    Task<bool> IsEmailExist<TUser>(string email, Guid? id = null) where TUser : User;
    Task<bool> ChangeBlockStatus<TUser>(Guid id) where TUser : User;
    string GenerateAccessToken(User user, IList<string> roles);
    Task<TokenResult> GenerateRefreshToken(Guid userId);
    Task<IdentityResult> AddWithRole(User user, Role role, string password);
    Task TryModifyPassword(User user, string? newPassword);
}
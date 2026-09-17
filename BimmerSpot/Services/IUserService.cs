using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using OneOf;
using OneOf.Types;

namespace BimmerSpot.Services;

public interface IUserService
{
    Task<ApplicationUser> GetCurrentUserAsync();

    List<ApplicationUser> GetAllUsers();

    Task<OneOf<Success, Failure>> DeleteUserAsync(ApplicationUser user);

    Task<OneOf<Success, Failure>> SetUserLockingAsync(ApplicationUser user, bool lockingEnabled);
}
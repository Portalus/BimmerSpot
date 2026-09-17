using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;
using System.Diagnostics;

namespace BimmerSpot.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        IHttpContextAccessor contextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        if (_contextAccessor.HttpContext is null)
        {
            throw new UnreachableException("User context is null");
        }

        var userPrincipal = _contextAccessor.HttpContext.User;

        var loggedUser = await _userManager.GetUserAsync(userPrincipal);

        if (loggedUser is null)
        {
            throw new UnreachableException("No logged user");
        }

        return loggedUser;
    }

    public List<ApplicationUser> GetAllUsers() =>
        _userManager.Users
            .OrderByDescending(x => x.CreatedDate)
            .ToList();

    public async Task<OneOf<Success, Failure>> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.Succeeded
            ? new Success()
            : new Failure(result.Errors.FirstOrDefault()?.Description ?? "Błąd podczas usuwania użytkownika");
    }

    public async Task<OneOf<Success, Failure>> SetUserLockingAsync(
        ApplicationUser user,
        bool lockingEnabled)
    {
        var userIsCurrentlyLocked = await _userManager.IsLockedOutAsync(user);

        if ((userIsCurrentlyLocked && lockingEnabled) ||
            (!userIsCurrentlyLocked && !lockingEnabled))
        {
            return new Success();
        }

        var result = await _userManager.SetLockoutEndDateAsync(
            user,
            lockingEnabled
                ? DateTimeOffset.MaxValue
                : null);

        return result.Succeeded
            ? new Success()
            : new Failure(result.Errors.FirstOrDefault()?.Description ?? "Nieznany błąd");
    }
}

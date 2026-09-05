using BimmerSpot.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace BimmerSpot.Services;

public class UserService : IUserService
{
    public IHttpContextAccessor _contextAccessor { get; }
    public UserManager<ApplicationUser> _userManager { get; }

    public UserService(
        IHttpContextAccessor contextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetCurrentUser()
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
}

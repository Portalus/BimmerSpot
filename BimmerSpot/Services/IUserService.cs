using BimmerSpot.Data.Models;

namespace BimmerSpot.Services;

public interface IUserService
{
    Task<ApplicationUser> GetCurrentUser();
}
using BimmerSpot.Data.Models;
using BimmerSpot.Models;
using BimmerSpot.Models.OneOf;
using OneOf;
using OneOf.Types;

namespace BimmerSpot.Services;

public interface ISpotService
{
    Task<OneOf<CreatedSpotDto, Failure>> CreateSpotAsync(CreateSpotDto createSpotDto);

    Task<List<Spot>> GetIncommingSpotsAsync();

    Task<List<Spot>> GetPastSpotsAsync();

    Task<OneOf<Success, LimitReached, UserAlreadyExist>> AddSpotAttendantAsync(
        Spot spot,
        ApplicationUser userToAdd);

    Task<OneOf<Success, UserNotOnTheList>> RemoveSpotAttendantAsync(
        Spot spot,
        ApplicationUser userToRemove);
}
using BimmerSpot.Models;
using BimmerSpot.Models.OneOf;
using OneOf;

namespace BimmerSpot.Services;

public interface ISpotService
{
    Task<OneOf<CreatedSpotDto, Failure>> CreateSpotAsync(CreateSpotDto createSpotDto);
}
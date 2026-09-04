using BimmerSpot.Data.Models;
using BimmerSpot.Models;
using BimmerSpot.Models.OneOf;

namespace BimmerSpot.Mappers;

public static class SpotExtensions
{
    public static Spot ToSpot(this CreateSpotDto model) =>
        new Spot()
        {
            StartDateTime = model.StartDateTime,
            City = model.City,
            StreetAndNumber = model.StreetAndNumber,
            Description = model.Description,
            CreatedBy = model.CreatedBy,
            Attendants = [model.CreatedBy]
        };

    public static CreatedSpotDto ToCreatedSpotDto(this Spot model) =>
        new CreatedSpotDto()
        {
            Id = model.Id,
            StartDateTime = model.StartDateTime,
            City = model.City,
            StreetAndNumber = model.StreetAndNumber,
            Description = model.Description,
            CreatedBy = model.CreatedBy!,
            Attendants = model.Attendants
        };
}

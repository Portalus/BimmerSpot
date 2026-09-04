using BimmerSpot.Data.Models;

namespace BimmerSpot.Models;

public record CreateSpotDto
{
    public required DateTime StartDateTime { get; set; }

    public required string City { get; set; }

    public required string StreetAndNumber { get; set; }

    public string? Description { get; set; }

    public required ApplicationUser CreatedBy { get; set; }
}

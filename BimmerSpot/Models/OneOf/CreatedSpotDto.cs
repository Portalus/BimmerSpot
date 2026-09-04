using BimmerSpot.Data.Models;

namespace BimmerSpot.Models.OneOf;

public class CreatedSpotDto
{
    public required int Id { get; set; }

    public required DateTime StartDateTime { get; set; }

    public required string City { get; set; }

    public required string StreetAndNumber { get; set; }

    public string? Description { get; set; }

    public required ApplicationUser CreatedBy { get; set; }

    public required List<ApplicationUser> Attendants { get; set; }
}
namespace BimmerSpot.Data.Models;

public class Spot
{
    public int Id { get; set; }

    public required DateTime StartDate { get; set; }

    public required string Location { get; set; }

    public string? Description { get; set; }

    public required ApplicationUser CreatedBy { get; set; }

    public List<ApplicationUser> Attendants { get; set; } = [];
}

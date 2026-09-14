namespace BimmerSpot.Data.Models;

public class Message
{
    public int Id { get; set; }

    public required string Sender { get; set; }

    public required string Content { get; set; }

    public required DateTime CreatedDate { get; set; }
}

namespace BimmerSpot.Models.OneOf;

public record Failure
{
    public string ErrorDescription { get; set; }

    private Failure()
    {
        ErrorDescription = string.Empty;
    }

    public Failure(string errorDescription)
    {
        ErrorDescription = errorDescription;
    }
}

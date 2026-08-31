using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BimmerSpot.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [MaxLength(30)]
    public required string FullName { get; set; }

    [MaxLength(30)]
    public string? CarModel { get; set; }
}


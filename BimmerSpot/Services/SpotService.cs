using BimmerSpot.Data;
using BimmerSpot.Data.Models;
using BimmerSpot.Mappers;
using BimmerSpot.Models;
using BimmerSpot.Models.OneOf;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace BimmerSpot.Services;

public class SpotService : ISpotService
{
    private ApplicationDbContext _dbContext;
    private UserManager<ApplicationUser> _userManager;

    public SpotService(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<OneOf<CreatedSpotDto, Failure>> CreateSpotAsync(CreateSpotDto createSpotDto)
    {
        var newSpot = createSpotDto.ToSpot();

        await _dbContext.Spots.AddAsync(newSpot);
        await _dbContext.SaveChangesAsync();

        return newSpot.ToCreatedSpotDto();
    }

    public async Task<List<Spot>> GetIncommingSpotsAsync() =>
        await _dbContext.Spots
            .Where(s => s.StartDateTime > DateTime.Now.AddHours(-1))
            .Include(s => s.Attendants)
            .ToListAsync();

    public async Task<List<Spot>> GetPastSpotsAsync() =>
        await _dbContext.Spots
            .Where(s => s.StartDateTime < DateTime.Now)
            .Include(s => s.Attendants)
            .ToListAsync();
}
using BimmerSpot.Data;
using BimmerSpot.Data.Models;
using BimmerSpot.Mappers;
using BimmerSpot.Models;
using BimmerSpot.Models.OneOf;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

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

    public async Task<List<Spot>> GetIncommingSpotsAsync()
    {
        var limit = DateTime.Now.AddHours(-1);

        return await _dbContext.Spots
            .Where(s => s.StartDateTime > limit)
            .Include(s => s.Attendants)
            .OrderBy(x => x.StartDateTime)
            .ToListAsync();
    }

    public async Task<List<Spot>> GetPastSpotsAsync()
    {
        var limit = DateTime.Now.AddHours(-1);

        return await _dbContext.Spots
            .Where(s => s.StartDateTime < limit)
            .Include(s => s.Attendants)
            .OrderByDescending(x => x.StartDateTime)
            .ToListAsync();
    }

    public async Task<OneOf<Success, LimitReached, UserAlreadyExist>> AddSpotAttendantAsync(
        Spot spot,
        ApplicationUser userToAdd)
    {
        if (spot.Attendants.Count == 10)
        {
            return new LimitReached();
        }

        if (spot.Attendants.Any(a => a.Id == userToAdd.Id))
        {
            return new UserAlreadyExist();
        }

        spot.Attendants.Add(userToAdd);
        await _dbContext.SaveChangesAsync();

        return new Success();
    }

    public async Task<OneOf<Success, UserNotOnTheList>> RemoveSpotAttendantAsync(
        Spot spot,
        ApplicationUser userToRemove)
    {
        if (!spot.Attendants.Contains(userToRemove))
        {
            return new UserNotOnTheList();
        }

        spot.Attendants.Remove(userToRemove);

        if (spot.Attendants.Count == 0)
        {
            _dbContext.Remove(spot);
            await _dbContext.SaveChangesAsync();
        }

        await _dbContext.SaveChangesAsync();

        return new Success();
    }
}
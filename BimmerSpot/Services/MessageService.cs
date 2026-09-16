using BimmerSpot.Data;
using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace BimmerSpot.Services;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _dbContext;

    public MessageService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OneOf<Message, Failure>> CreateMessageAsync(string sender, string content)
    {
        if (string.IsNullOrWhiteSpace(sender) ||
            string.IsNullOrWhiteSpace(content))
        {
            return new Failure("Message sender and content must be provided");
        }

        var message = new Message()
        {
            Sender = sender,
            Content = content,
            CreatedDate = DateTime.Now,
        };

        await _dbContext.AddAsync(message);
        await _dbContext.SaveChangesAsync();

        return message;
    }

    public async Task<List<Message>> GetAllMessagesAsync() =>
        await _dbContext.Messages.ToListAsync();

    public async Task<OneOf<Success, Failure>> DeleteMessageAsync(Message message)
    {
        try
        {
            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync();

            return new Success();
        }
        catch (Exception ex)
        {
            return new Failure(ex.Message);
        }
    }
}

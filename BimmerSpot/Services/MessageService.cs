using BimmerSpot.Data;
using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using OneOf;

namespace BimmerSpot.Services;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _context;

    public MessageService(ApplicationDbContext context)
    {
        _context = context;
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

        await _context.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }
}

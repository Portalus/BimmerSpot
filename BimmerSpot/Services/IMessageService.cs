using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using OneOf;
using OneOf.Types;

namespace BimmerSpot.Services;

public interface IMessageService
{
    Task<OneOf<Message, Failure>> CreateMessageAsync(string sender, string content);

    Task<List<Message>> GetAllMessagesAsync();

    Task<OneOf<Success, Failure>> DeleteMessageAsync(Message message);
}
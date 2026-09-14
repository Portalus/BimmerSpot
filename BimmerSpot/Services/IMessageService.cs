using BimmerSpot.Data.Models;
using BimmerSpot.Models.OneOf;
using OneOf;

namespace BimmerSpot.Services;

public interface IMessageService
{
    Task<OneOf<Message, Failure>> CreateMessageAsync(string sender, string content);
}
using Microsoft.AspNetCore.Identity;

namespace BimmerSpot.Utilities;

public class PolishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName) =>
        new()
        {
            Code = nameof(DuplicateUserName),
            Description = $"Nazwa użytkownika '{userName}' jest już zajęta."
        };

    public override IdentityError DuplicateEmail(string email) =>
        new()
        {
            Code = nameof(DuplicateEmail),
            Description = $"Adres e-mail '{email}' jest już używany."
        };
}
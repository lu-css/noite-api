using Noite.DTO;

namespace Noite.Providers.TokenProvider;

public interface ITokenProvider
{
    public string Hash(UserDTO user);
}


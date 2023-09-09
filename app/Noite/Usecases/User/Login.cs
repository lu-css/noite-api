using Noite.Providers.Criptografy;
using Noite.Models;
using Noite.Repositories;

namespace Noite.Usecases.User;

public class Login
{
    private readonly UserRepository _userRepository;
    private readonly ICriptografyProvider _criptografy;

    public Login(UserRepository userRepository)
    {
        _userRepository = userRepository;
        _criptografy = new Pbkdf2();
    }

    public async Task<UserModel> Execute(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user == null)
        {
            throw new Exception($"User with Email \"{email}\" not founded.");
        }

        bool valid = _criptografy.VerifyPassword(password, user.Password ?? "", user.Salt ?? "");

        if (!valid)
        {
            throw new Exception($"Email or password wrong.");
        }

        return user;
    }
}

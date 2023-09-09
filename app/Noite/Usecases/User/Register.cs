using Noite.Providers.Criptografy;
using Noite.Models;
using Noite.Repositories;

namespace Noite.Usecases.User;

public class Register
{
    private readonly UserRepository _userRepository;
    private readonly ICriptografyProvider _criptografy;

    public Register(UserRepository userRepository)
    {
        _userRepository = userRepository;
        _criptografy = new Pbkdf2();
    }

    public async Task<UserModel> Execute(string? name, string? email, string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("Password cant be blank");
        }

        (string hashedPassword, string salt) = _criptografy.HashPassword(password);

        var user = new UserModel
        {
            Username = name,
            Email = email,
            Password = hashedPassword,
            Salt = salt,
            EmailConfirmed = false
        };

        await _userRepository.CreateAsync(user);
        return user;
    }
}

using Microsoft.AspNetCore.Mvc;
using Noite.Repositories;
using Noite.Models;
using Noite.DTO;
using User = Noite.Usecases.User;
using Noite.Providers.TokenProvider;

namespace Noite.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;

    public AccountController(UserRepository userRepository)
    {
        _userRepository = userRepository;
        _tokenProvider = new Jwt();
    }
    [HttpGet]
    public async Task<List<UserDTO>> Get()
    {
        var users = await _userRepository.GetAsync();

        return users
          .Select(user => new UserDTO(user.Id, user.Username, user.Email, user.Password))
          .ToList();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<UserDTO>> Get(string id)
    {
        var user = await _userRepository.GetAsync(id);

        if (user == null)
        {
            return NotFound("User not founded");
        }

        user.Password = "";
        user.Salt = "";

        return new UserDTO(user.Id, user.Username, user.Email, "");
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserModel newUser)
    {
        var user = await new User.Register(_userRepository).Execute(newUser.Username, newUser.Email ?? "", newUser.Password ?? "");

        return CreatedAtAction(nameof(Get), new { id = user.Id }, new UserDTO(user.Id, user.Username, user.Email, ""));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<TokenDTO>> Login(UserModel newUser)
    {
        var user = await new User.Login(_userRepository).Execute(newUser.Email ?? "", newUser.Password ?? "");

        var userDto = new UserDTO(user.Id, user.Username, user.Email, "");
        var token = _tokenProvider.Hash(userDto);

        return new TokenDTO(token, userDto);
    }
}

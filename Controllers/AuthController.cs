using last_try_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        // Check if the email is already registered
        var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return Conflict(new { message = "Email already registered" });
        }

        // Hash the password before saving to the database (implement password hashing logic)
        // For example, using ASP.NET Core's built-in PasswordHasher
        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, user.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginModel userLoginModel)
    {
        // Find the user by username (or email, depending on your login logic)
        var user = _context.Users.SingleOrDefault(u => u.Username == userLoginModel.Email);

        // Check if the user exists
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, userLoginModel.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        // Implement authentication token generation logic here (JWT, for example)
        var token = "your_generated_token_here";

        return Ok(new { message = "Login successful", token });
    }
}

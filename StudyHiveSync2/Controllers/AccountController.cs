//New
using Microsoft.AspNetCore.Mvc;//give ControllerBase inherit function
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudyHiveSync2.Model;
using StudyHiveSync2.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
//using StudyHiveSync2.Services;
using BCrypt.Net;

namespace StudyHiveSync2.Controllers
{
    [ApiController]                                                      // Indicates that this class is an API controller
    [Route("[controller]")]                                              // maps url with controller
    public class AccountController : ControllerBase                      // Inherits from ControllerBase, which provides basic functionality for API controllers
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;                                          // Dependency injection for JWT token generator
        private readonly ApplicationDbContext _context;                                                  // Dependency injection for the database context
        private readonly EmailService _emailService;                                                     // Dependency injection for the email service
        private readonly ILogger<AccountController> _logger;                                             //DI for logging

        //below contructor is used for dependency initialization
        public AccountController(IJwtTokenGenerator jwtTokenGenerator, ApplicationDbContext context, EmailService emailService, ILogger<AccountController> logger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("login")]                                                                                          // Defines a POST endpoint for login //this is route
        public async Task<IActionResult> Login(LoginDto model)
        {
            // Validate user credentials
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);                        //The method queries the database asynchronously to find a user with the specified email using Entity Framework Core's FirstOrDefaultAsync method.
                                                                                                                     //The await keyword pauses the method execution until the query completes.
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))                            //if the user was found
            {
                _logger.LogWarning("Invalid login attempt for {Email}", model.Email);                                //if credentials wrong then, it logs a warning message.
                return Unauthorized();                                                                               // Return 401 Unauthorized if credentials are invalid
            }

            var token = _jwtTokenGenerator.GenerateJwtToken(user);                                                   // Generate a JWT token for users
            _logger.LogInformation("User logged in successfully: {Email}", user.Email);                              //logs an info message indicating a successful login.
            return Ok(new { Token = token });                                                                        // Return 200 ok with the token in the response
        }

        [HttpPost("register")]                                                                                       // Defines a POST endpoint for registration
        public async Task<IActionResult> Register([FromBody] SignupDto model)                                        //The Register method takes a SignupDto model as a parameter, which contains the user's registration information.
        {
            if (ModelState.IsValid)                                                                                  // Check if the model state is valid(This ensures that the incoming data meets the required validation rules.)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);                                 // Hash the user's password
                                                                                                                     // Hashing: The method hashes the user's password using BCrypt to ensure it is securely stored in the database.
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = hashedPassword,
                    AccountType = model.AccountType,
                    Image = model.Image,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);                                                                           // Add the user to the database context
                await _context.SaveChangesAsync();                                                                  //The changes are saved to the database asynchronously.
                //_context.SaveChanges();


                                                                                                                    //Email Service: The method sends a welcome email to the user after successful registration.
                if (_emailService != null)                                                                          //The method checks if the email service is available.
                {
                    var subject = "Registration Successful";
                    var message = $"Dear {user.Name}, Welcome to StudyHiveSync Platform. We are excited to have you on board!";
                    await _emailService.SendEmailAsync(user.Email, subject, message);                               //The email is sent asynchronously.
                    _logger.LogInformation("Email sent to : {Email}", user.Email);                                 //Logs the email sending status.
                }
                else
                {
                                                                                                                   // Handle the case where _emailService is null
                    _logger.LogWarning("Email service is not available");
                    return StatusCode(500, "Email service is not available.");
                }

                _logger.LogInformation("User registered successfully: {Email}", user.Email);
                return Ok(new { token = "User Created" });
            }

                                                                                                                   //If the email service is not available, it logs a warning and returns a 500 Internal Server Error response.
            _logger.LogError("Invalid registration attempt");
            return BadRequest(ModelState);
        }
    }

    public interface IJwtTokenGenerator                                                                           //IJwtTokenGenerator defines a contract for generating JWT tokens.
    {
        string GenerateJwtToken(User user);                                                                       // GenerateJwtToken that takes a User object and returns a string (the JWT token).
    }

    public class JwtTokenGenerator : IJwtTokenGenerator                                                           //JwtTokenGenerator implements the IJwtTokenGenerator interface.
    {
        private readonly byte[] _key;                                                                            // _key holds the secret key used for signing the JWT.

        public JwtTokenGenerator(byte[] key) 
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));                                          //null check
                                                                                                                //It assigns the key to the _key field and throws an ArgumentNullException if the key is null.
        }

        public string GenerateJwtToken(User user)                                                               //implements interface method
        {
            var tokenHandler = new JwtSecurityTokenHandler();                                                   //JwtSecurityTokenHandler is used to create and write JWT tokens.
            var tokenDescriptor = new SecurityTokenDescriptor                                                   //SecurityTokenDescriptor describes the properties of the token.
            {
                Subject = new ClaimsIdentity(new[]                                                             //ClaimIdentity is array of claims,- Claims are key-value pairs that represent information about the user.
                {
                    new Claim(ClaimTypes.Name, user.Name),                                                    //users name
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),                             //users id
                    new Claim(ClaimTypes.Role, user.AccountType)                                              //The user's role or account type.
                }),
                Expires = DateTime.UtcNow.AddDays(7),                                                         //The token is set to expire in 7 days (DateTime.UtcNow.AddDays(7)).
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)                      //SigningCredentials specifies the key and algorithm used to sign the token.
                                                                                                                                                         //SecurityAlgorithms.HmacSha256Signature specifies the HMAC SHA-256 algorithm for signing.
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);                                                                                      //CreateToken method of JwtSecurityTokenHandler creates the token based on the descriptor.
            return tokenHandler.WriteToken(token);                                                                                                      // WriteToken method converts the token to a string and returns it.
        }
    }
}


//The async keyword is used to define an asynchronous method. It allows the method to run asynchronously, meaning it can perform tasks without blocking the main thread.
//When a method is marked with async, it can use the await keyword to pause its execution until the awaited task completes. Best used in I/O Operations calls or web requests.
//A Task represents an asynchronous operation. 
//IActionResult is an interface in ASP.NET Core that represents the result of an action method. 
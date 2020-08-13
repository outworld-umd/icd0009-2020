using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO;
using PublicApi.DTO.Identity;
using AppUser = Domain.App.Identity.AppUser;

namespace WebApp.ApiControllers._1._0.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public AccountsController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountsController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="model">login data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new JwtResponseDTO() {
                    Token = jwt, 
                    Status = "Logged in", 
                    FirstName = appUser.FirstName, 
                    LastName = appUser.LastName, 
                    Roles = _userManager.GetRolesAsync(appUser)?.Result ?? new Collection<string>()});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }


        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation) 
        /// </summary>
        /// <param name="dto">user data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"WebApi register. User {dto.Email} already registered!");
                return NotFound(new MessageDTO("User already registered!"));
            }

            appUser = new AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            await _userManager.AddToRolesAsync(appUser, dto.Roles);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {appUser.Email} created a new account with password.");
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJWT(
                        claimsPrincipal.Claims
                            .Append(new Claim(ClaimTypes.GivenName, appUser.FirstName))
                            .Append(new Claim(ClaimTypes.Surname, appUser.LastName)),
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                    _logger.LogInformation($"WebApi register. User {user.Email} logged in.");
                    return Ok(new JwtResponseDTO()
                    {
                        Token = jwt, 
                        Status = $"User {user.Email} created and logged in.",
                        FirstName = appUser.FirstName, 
                        LastName = appUser.LastName,
                        Roles = _userManager.GetRolesAsync(appUser)?.Result ?? new Collection<string>()
                    });
                }

                _logger.LogInformation($"User {appUser.Email} not found after creation!");
                return BadRequest(new MessageDTO("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new MessageDTO() {Messages = errors});
        }
    }
}
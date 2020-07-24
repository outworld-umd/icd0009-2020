using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Identity;
using AppUser = Domain.App.Identity.AppUser;

namespace WebApp.ApiControllers._1._0.Identity
{
[ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountsController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
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


        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            throw new NotImplementedException();
        }

        public class LoginDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class RegisterDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
        }
    }
}
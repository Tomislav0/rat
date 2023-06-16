using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Portfolio.DAL.BM;
using Portfolio.DAL.DTO;
using Portfolio.DAL.Models.Account;
using Portfolio.WebAPI.JwtFeatures;
using System.IdentityModel.Tokens.Jwt;

namespace Portfolio.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        public AccountController(UserManager<User> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationBM userForAuthentication)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                    return Unauthorized(new AuthResponseDTO { ErrorMessage = "Invalid Authentication" });

                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponseDTO { IsAuthSuccessful = true, Token = token });
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationBM userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = new User { FirstName = userForRegistration.FirstName, LastName = userForRegistration.LastName, Email = userForRegistration.Email, UserName = userForRegistration.Email };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }

            return StatusCode(201);
        }
    }
}

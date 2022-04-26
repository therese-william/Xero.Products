using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xero.Products.App.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public TokenController(IConfiguration config, ITokenService tokenService, IUserRepository userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("Invalid credentials");
            }
            
            var validUser = await GetUser(userModel);

            if(validUser == null)
            {
                return BadRequest("Invalid credentials");
            }
            
            var token = _tokenService.BuildToken(_config["JwtSecret"].ToString(), validUser);
            return Ok(token);
        }

        private async Task<UserDTO?> GetUser(UserModel userModel)
        {
            // Write your code here to authenticate the user     
            return _userRepository.GetUser(userModel);
        }
    }
}

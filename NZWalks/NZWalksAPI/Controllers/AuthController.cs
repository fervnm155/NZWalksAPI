using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            //check if user is authenticated
            //check username and password
            var user = await userRepository.Authenticate(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //generate a JWT token
                var token = await tokenHandler.GetToken(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is incorrect");
        }
    }
}

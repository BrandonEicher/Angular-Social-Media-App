using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase 
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService service)
        {
            _logger = logger;
            _authService = service;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(User user) 
        {
            if (user == null || !ModelState.IsValid) {
                return BadRequest();
            }
            _authService.CreateUser(user);
            return NoContent();
        }

        [HttpGet]
        [Route("login")]
        public ActionResult<string> SignIn(string username, string password) 
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest();
            }

            var token = _authService.SignIn(username, password);

            if (string.IsNullOrWhiteSpace(token)) {
                return Unauthorized();
            }

            return Ok(token);
        }
    } 
}

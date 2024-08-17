using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class XController : ControllerBase 
    {
        private readonly ILogger<XController> _logger;
        private readonly IXRepository _xRepository;

        public XController(ILogger<XController> logger, IXRepository repository)
        {
            _logger = logger;
            _xRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<X>> GetX() 
        {
            return Ok(_xRepository.GetAllX());
        }

        [HttpGet("user/{username}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<X>> GetUserPosts(string username)
        {
            var userPosts = _xRepository.GetUserPosts(username);
            if (userPosts == null)
            {
                return NotFound();
            }
            return Ok(userPosts);
        }

        [HttpGet("{xId}")]
        public ActionResult<X> GetXById(string xId) 
        {
            var x = _xRepository.GetXById(xId);
            if (x == null) {
                return NotFound();
            }
            return Ok(x);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<X> CreateX(X x) 
        {
            if (!ModelState.IsValid || x == null) {
                return BadRequest();
            }
            
            var username = User.FindFirst("Username")?.Value;

            if (string.IsNullOrEmpty(username)) {
                return Unauthorized();
            }

            x.Username = username;

            if (x.Date == DateTime.MinValue)
            {
                x.Date = DateTime.Now;
            }

            var newX = _xRepository.CreateX(x);
            return CreatedAtAction(nameof(GetXById), new { xId = newX.XId }, newX);
        }


        [HttpPut("{xId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<X> UpdateX(string xId, X x) 
        {
            if (!ModelState.IsValid || x == null) {
                return BadRequest();
            }
            
            var existingX = _xRepository.GetXById(xId);
            if (existingX == null) {
                return NotFound();
            }

            existingX.Text = x.Text;
            existingX.Username = x.Username;
            existingX.Date = x.Date;

            _xRepository.UpdateX(existingX);

            return Ok(existingX);
        }


        [HttpDelete("{xId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult DeleteX(string xId) 
        {
            var existingX = _xRepository.GetXById(xId);
            if (existingX == null) {
                return NotFound();
            }

            _xRepository.DeleteXById(xId); 
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Improve.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Improve.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                _context.Users.Add(new Models.User { UserName = user.UserName, Password = user.Password });
                await _context.SaveChangesAsync();

                return CreatedAtAction("CreateUser", new { Name = user.UserName, Password = user.Password });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost("{login}")]
        public ActionResult Login(User user)
        {
            bool isRegisterUser=_context.Users.Where(para => para.UserName == user.UserName && para.Password == user.Password).Any();

            if (isRegisterUser)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized(user);
            }
        }
    }
}

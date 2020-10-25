using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Improve.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetTodoItem(string userName)
        {
            _context.User.Add(new Models.User { id = 4, Name = "tt" ,Password="ddd"});
            _context.SaveChanges();
            var todoItem =  _context.User.FirstOrDefault(para=>para.Name==userName);

            if (todoItem == null)
            {
                return Ok();
            }
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(string userName,string password)
        {
            _context.User.Add(new Models.User { Name = userName, Password = password });
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateUser", new { Name = userName, Password = password });
        }
    }
}

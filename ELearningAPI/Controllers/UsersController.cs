using ELearningAPI.Data;
using ELearningAPI.DTOS;
using ELearningAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace ELearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ElearningReusableContext _context;
        public UsersController(ElearningReusableContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user != null && loginDto.Password == user.Password)
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return Conflict("Email is already registered");
            }

            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Address = user.Address,
                Area = user.Area,
                MobileNumber = user.MobileNumber,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }
    }
}



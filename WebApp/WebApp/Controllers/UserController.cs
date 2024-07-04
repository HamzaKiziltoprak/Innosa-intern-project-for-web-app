// Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;

            // Example users for initial data
            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { ADI = "John", SOYADI = "Doe", KULLANICI_ADI = "johndoe", SIFRE = "password" });
                _context.Users.Add(new User { ADI = "Jane", SOYADI = "Doe", KULLANICI_ADI = "janedoe", SIFRE = "password" });
                _context.SaveChanges();
            }
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        { 
            var res = await _context.Users.Select(u => new User
            {
                ID = u.ID,
                ADI = u.ADI,
                SOYADI = u.SOYADI,
                KULLANICI_ADI = u.KULLANICI_ADI
            }).ToListAsync();

            return res;
        }
    }
}
// bu kodlar ne işe yarar : 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHiveSync2.Data;
using StudyHiveSync2.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyHiveSync2.Controllers
{
    [Route("api/[controller]")] //set route to api/users
    [ApiController] //indicates that this class is an API controller
    public class UsersController : ControllerBase //DI using constructor injection
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] 
        [Authorize(Roles = "Admin")]
        //IEnumerable<T> is an interface that represents a collection of elements that can be enumerated (i.e., iterated over)
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()//indicates that the method returns a collection of UserDto objects that can be enumerated.
        {
            return await _context.Users
                .Select(user => new UserDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    AccountType = user.AccountType,
                    Image = user.Image
                })
                .ToListAsync();
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Select(user => new UserDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    AccountType = user.AccountType,
                    Image = user.Image
                })
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("UpdateUserById/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutUser(UpdateUserDto userDto)
        {
           
            var user = await _context.Users.FindAsync(userDto.UserId);
            if (user == null)
            {
                return NotFound();
            }

           
            try
            {
                user.Name = userDto.Name;
                user.Email = userDto.Email;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userDto.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}




//[HttpPost]
//[Authorize(Roles = "User")]
//public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
//{
//    var user = new User
//    {
//        Name = userDto.Name,
//        Email = userDto.Email,
//        Password = "defaultPassword", // Set a default or hashed password
//        AccountType = userDto.AccountType,
//        Image = userDto.Image,
//        CreatedAt = userDto.CreatedAt,
//        UpdatedAt = userDto.UpdatedAt
//    };

//    _context.Users.Add(user);
//    await _context.SaveChangesAsync();

//    userDto.UserId = user.UserId;

//    return CreatedAtAction("GetUser", new { id = userDto.UserId }, userDto);
//}







//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudyHiveSync2.Data;
//using StudyHiveSync2.Model;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace StudyHiveSync2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public UsersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }


//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
//        {
//            return await _context.Users
//                .Select(user => new UserDto
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Password = user.Password, // Added this line
//                    AccountType = user.AccountType,
//                    Image = user.Image,
//                    CreatedAt = user.CreatedAt,
//                    UpdatedAt = user.UpdatedAt
//                })
//                .ToListAsync();
//        }
//        //[Authorize(Roles ="User")]
//        [HttpGet("GetUserById")]
//        public async Task<ActionResult<UserDto>> GetUser(int id)
//        {
//            var user = await _context.Users
//                .Select(user => new UserDto
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Password = user.Password, // Added this line
//                    AccountType = user.AccountType,
//                    Image = user.Image,
//                    CreatedAt = user.CreatedAt,
//                    UpdatedAt = user.UpdatedAt
//                })
//                .FirstOrDefaultAsync(u => u.UserId == id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return user;
//        }

//        //[HttpGet]
//        //public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
//        //{
//        //    return await _context.Users
//        //        .Select(user => new UserDto
//        //        {
//        //            UserId = user.UserId,
//        //            Name = user.Name,
//        //            Email = user.Email,
//        //            AccountType = user.AccountType,
//        //            Image = user.Image,
//        //            CreatedAt = user.CreatedAt,
//        //            UpdatedAt = user.UpdatedAt
//        //        })
//        //        .ToListAsync();
//        //}

//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<UserDto>> GetUser(int id)
//        //{
//        //    var user = await _context.Users
//        //        .Select(user => new UserDto
//        //        {
//        //            UserId = user.UserId,
//        //            Name = user.Name,
//        //            Email = user.Email,
//        //            AccountType = user.AccountType,
//        //            Image = user.Image,
//        //            CreatedAt = user.CreatedAt,
//        //            UpdatedAt = user.UpdatedAt
//        //        })
//        //        .FirstOrDefaultAsync(u => u.UserId == id);

//        //    if (user == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return user;
//        //}

//        [HttpPost("AddUser")]
//        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
//        {
//            var user = new User
//            {
//                Name = userDto.Name,
//                Email = userDto.Email,
//                Password = "defaultPassword", // Set a default or hashed password
//                AccountType = userDto.AccountType,
//                Image = userDto.Image,
//                CreatedAt = userDto.CreatedAt,
//                UpdatedAt = userDto.UpdatedAt
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            userDto.UserId = user.UserId;

//            return CreatedAtAction("GetUser", new { id = userDto.UserId }, userDto);
//        }
//        //[Authorize(Roles ="User")]
//        [HttpPut("UpdateUserById")]
//        public async Task<IActionResult> PutUser(int id, UserDto userDto)
//        {
//            if (id != userDto.UserId)
//            {
//                return BadRequest();
//            }

//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            user.Name = userDto.Name;
//            user.Email = userDto.Email;
//            user.AccountType = userDto.AccountType;
//            user.Image = userDto.Image;
//            user.UpdatedAt = userDto.UpdatedAt;

//            _context.Entry(user).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        //[Authorize(Roles="User")]
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            _context.Users.Remove(user);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UserExists(int id)
//        {
//            return _context.Users.Any(e => e.UserId == id);
//        }
//    }
//}





//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudyHiveSync2.Data;
//using StudyHiveSync2.Model;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace StudyHiveSync2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public UsersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
//        {
//            return await _context.Users
//                .Select(user => new UserDto
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Password = user.Password, // Added this line
//                    AccountType = user.AccountType,
//                    Image = user.Image,
//                    CreatedAt = user.CreatedAt,
//                    UpdatedAt = user.UpdatedAt
//                })
//                .ToListAsync();
//        }

//        [HttpGet("GetUserById")]
//        public async Task<ActionResult<UserDto>> GetUser(int id)
//        {
//            var user = await _context.Users
//                .Select(user => new UserDto
//                {
//                    UserId = user.UserId,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Password = user.Password, // Added this line
//                    AccountType = user.AccountType,
//                    Image = user.Image,
//                    CreatedAt = user.CreatedAt,
//                    UpdatedAt = user.UpdatedAt
//                })
//                .FirstOrDefaultAsync(u => u.UserId == id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return user;
//        }

//        [HttpPost]
//        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
//        {
//            var user = new User
//            {
//                Name = userDto.Name,
//                Email = userDto.Email,
//                Password = "defaultPassword", // Set a default or hashed password
//                AccountType = userDto.AccountType,
//                Image = userDto.Image,
//                CreatedAt = userDto.CreatedAt,
//                UpdatedAt = userDto.UpdatedAt
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            userDto.UserId = user.UserId;

//            return CreatedAtAction("GetUser", new { id = userDto.UserId }, userDto);
//        }

//        [HttpPut("GetUserById")]
//        public async Task<IActionResult> PutUser(int id, UserDto userDto)
//        {
//            if (id != userDto.UserId)
//            {
//                return BadRequest();
//            }

//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            user.Name = userDto.Name;
//            user.Email = userDto.Email;
//            user.AccountType = userDto.AccountType;
//            user.Image = userDto.Image;
//            user.UpdatedAt = userDto.UpdatedAt;

//            _context.Entry(user).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpDelete("DeleteUser")]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            _context.Users.Remove(user);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UserExists(int id)
//        {
//            return _context.Users.Any(e => e.UserId == id);
//        }
//    }
//}






////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;
////using StudyHiveSync2.Data;
////using StudyHiveSync2.Model;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;

////namespace StudyHiveSync2.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class UsersController : ControllerBase
////    {
////        private readonly ApplicationDbContext _context;

////        public UsersController(ApplicationDbContext context)
////        {
////            _context = context;
////        }

////        [HttpGet]
////        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
////        {
////            return await _context.Users.ToListAsync();
////        }

////        [HttpGet("{id}")]
////        public async Task<ActionResult<User>> GetUser(int id)
////        {
////            var user = await _context.Users.FindAsync(id);

////            if (user == null)
////            {
////                return NotFound();
////            }

////            return user;
////        }

////        [HttpPost]
////        public async Task<ActionResult<User>> PostUser(User user)
////        {
////            _context.Users.Add(user);
////            await _context.SaveChangesAsync();

////            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
////        }

////        [HttpPut("{id}")]
////        public async Task<IActionResult> PutUser(int id, User user)
////        {
////            if (id != user.UserId)
////            {
////                return BadRequest();
////            }

////            _context.Entry(user).State = EntityState.Modified;

////            try
////            {
////                await _context.SaveChangesAsync();
////            }
////            catch (DbUpdateConcurrencyException)
////            {
////                if (!UserExists(id))
////                {
////                    return NotFound();
////                }
////                else
////                {
////                    throw;
////                }
////            }

////            return NoContent();
////        }

////        [HttpDelete("{id}")]
////        public async Task<IActionResult> DeleteUser(int id)
////        {
////            var user = await _context.Users.FindAsync(id);
////            if (user == null)
////            {
////                return NotFound();
////            }

////            _context.Users.Remove(user);
////            await _context.SaveChangesAsync();

////            return NoContent();
////        }

////        private bool UserExists(int id)
////        {
////            return _context.Users.Any(e => e.UserId == id);
////        }
////    }
////}



////using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;

////namespace StudyHiveSync2.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class UserController : ControllerBase
////    {
////    }
////}

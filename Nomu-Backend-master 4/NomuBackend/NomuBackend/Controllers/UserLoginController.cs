using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NomuBackend.Dto;
using NomuBackend.Model;

namespace NomuBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserLoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    return BadRequest(new { Message = "Email cannot be null" });
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (user.UserName == null)
                    {
                        // Handle null userName case
                        return BadRequest("Username cannot be null");
                    }
                    if (model.Password == null)
                    {
                        // Handle null userName case
                        return BadRequest("Password cannot be null");
                    }

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "Login successful" });
                    }
                }
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await Task.Run(() => _userManager.Users.ToList());
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User updateUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.UserName = updateUser.Name;
            user.Email = updateUser.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { Message = "User Updated Successfully" });
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { Message = "User Deleted Successfully" });
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return BadRequest(ModelState);
            }
        }
    }
}

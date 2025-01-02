using Ecommerce_Core.DTOs.AuthDTO;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepository;

        public AuthController(IAuthRepo authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerDto.Username,
                Name = registerDto.FullName,
                Email = registerDto.Email,
            };

            var result = await _authRepository.RegisterAsync(user, registerDto.Password);

            if (result == "User registered successfully.")
            {
                return Ok(new { message = result });
            }

            // لو فشل التسجيل، رجّع الرسالة التفصيلية كـ BadRequest
            return BadRequest(new { errors = result });
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Log received input
                Console.WriteLine($"Login attempt: Username={loginDto.Username}");

                var token = await _authRepository.LoginAsync(loginDto.Username, loginDto.Password);

                // Log repository result
                Console.WriteLine($"Login result: {token}");

                if (token == "Invalid username or password.")
                {
                    return Unauthorized(new { Message = token });
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }




        [HttpPost("password-reset-request")]
        public async Task<IActionResult> PasswordResetRequest([FromBody] PasswordResetRequestDTO resetRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.SendPasswordResetLinkAsync(resetRequestDto.Email);
            if (result == "Password reset link sent.")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.ChangePasswordAsync(changePasswordDto.email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (result == "Password changed successfully.")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
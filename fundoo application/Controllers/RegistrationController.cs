using Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using Repository.Service;
using System.Data.SqlClient;



namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly IRegistrationBusiness Iregistration_bl;

        public RegistrationController(IRegistrationBusiness Iregistration_bl)
        {
            this.Iregistration_bl = Iregistration_bl;
        }

        [HttpGet]
        public async Task<IActionResult> Getregdetails()
        {
            try
            {
                var details = await Iregistration_bl.Getregdetails();
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Addusers(Registration users)
        {
            try
            {
                var details = await Iregistration_bl.Addusers(users);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{Email}")]
        public async Task<IActionResult> Deleteusers(string email)
        {
            try
            {
                var details = await Iregistration_bl.Deleteusers(email);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{Mail}")]
        public async Task<IActionResult> updateuser(string emial, Registration reg)
        {
            try
            {
                var details = await Iregistration_bl.updateuser(emial,reg);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> userLogin(string email, string password, IConfiguration configuration)
        {
            try
            {
                var details = await Iregistration_bl.userLogin(email, password, configuration);
                return Ok(details);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Mail}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var details = await Iregistration_bl.GetUserByEmail(email);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

      

     [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        try
        {
            await Iregistration_bl.ForgotPassword(request.Email);
            return Ok("Password reset email sent successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
            // Handle unique constraint violation (error number 2627)
            return StatusCode(500, "Email address is already registered.");
        }
        catch (SqlException ex)
        {
            // Handle other SQL-related exceptions
            return StatusCode(500, "An error occurred while accessing the database.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string otp, string newPassword)
        {
            try
            {
                await Iregistration_bl.ResetPassword(email, otp, newPassword);
                return Ok("Password reset successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while resetting the password.");
            }
        }
    




}
}

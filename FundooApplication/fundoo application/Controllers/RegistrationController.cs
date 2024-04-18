using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using ModelLayer.Response;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using Repository.Service;
using System.Data.SqlClient;
using System.Security.Claims;



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
                return Ok(new ResponseModel<IEnumerable<Registration>>
                {
                    Message = details != null && details.Any() ? "Users retrieved successfully" : "No Users found",
                    Data = details
                });
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return StatusCode(500, new ResponseModel<string>
                    {
                        Success = false,
                        Message = "An error occurred while retrieving Users from the database.",
                        Data = null
                    });
                }
                else
                {
                    return StatusCode(500, new ResponseModel<string>
                    {
                        Success = false,
                        Message = "An error occurred.",
                        Data = null
                    });
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Addusers(Registration users)
        {
            try
            {
                var details = await Iregistration_bl.Addusers(users);
                if (details>0)
                {
                    var response = new ResponseModel<Registration>
                    {
                        Success = true,
                        Message = "User Registration Successful"
                    };
                    return Ok(response);
                }
                else
                {

                    return BadRequest("invalid input");
                }
            }
            catch (Exception ex)
            {
                if (ex is DuplicateEmailException)
                {
                    var response = new ResponseModel<Registration>
                    {
                        Success = false,
                        Message = ex.Message
                    };
                    return BadRequest(response);


                }
                else if (ex is InvalidEmailFormatException)
                {
                    var response = new ResponseModel<Registration>
                    {
                        Success = false,
                        Message = ex.Message
                    };
                    return BadRequest(response);

                }
                else
                {
                    return StatusCode(500, $"An error occurred while adding the user: {ex.Message}");
                }
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Deleteusers()
        {
            try
            {
                // Get the userid of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(401, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int authenticatedUserId = int.Parse(userIdClaim.Value);

                // Proceed with deleting the user account
                var details = await Iregistration_bl.Deleteusers(authenticatedUserId);
                if (details > 0)
                {
                    return Ok(new ResponseModel<string>
                    {

                        Message = "User deleted  successfully",
                        Data = null,

                    });
                }
                else
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "User not found",
                        Data = null
                    });
                }
            }
            catch (DatabaseException ex)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                });
            }
        }




        [Authorize]
        [HttpPut]
        public async Task<IActionResult> updateuser(Registration reg)
        {
            try
            {
                // Get the userid of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(401, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int
                int authenticatedUserId = int.Parse(userIdClaim.Value);

                // Call the business logic layer to update the user's information
                var details = await Iregistration_bl.updateuser(authenticatedUserId, reg);

                var response = new ResponseModel<int>
                {

                    Message = "user updated successfully",
                    Data = details

                };

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return NotFound(response);
            }
            catch (DatabaseException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return StatusCode(500, response);
            }
            catch (RepositoryException ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message
                };
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred: " + ex.Message
                };
                return StatusCode(500, response);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> userLogin(string email, string password, IConfiguration configuration)
        {
            try
            {
                var details = await Iregistration_bl.userLogin(email, password, configuration);
                var response = new ResponseModel<string>
                {

                    Message = "Login Sucessfull",
                    Data = details

                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    var response = new ResponseModel<Registration>
                    {

                        Success = false,
                        Message = ex.Message

                    };
                    return Conflict(response);
                }
                else if (ex is InvalidPasswordException)
                {
                    var response = new ResponseModel<Registration>
                    {

                        Success = false,
                        Message = ex.Message

                    };
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, $"An error occurred while processing the login request: {ex.Message}");

                }
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var details = await Iregistration_bl.GetUserByEmail(email);

                return Ok(new ResponseModel<IEnumerable<Registration>>
                {
                    Message = details != null ? "User retrieved successfully" : "No User found",
                    Data = (IEnumerable<Registration>)details
                });

            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return StatusCode(500, new ResponseModel<string>
                    {
                        Success = false,
                        Message = "An error occurred while retrieving user from the database.",
                        Data = null
                    });
                }
                else
                {
                    return StatusCode(500, new ResponseModel<string>
                    {
                        Success = false,
                        Message = "An error occurred.",
                        Data = null
                    });
                }
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

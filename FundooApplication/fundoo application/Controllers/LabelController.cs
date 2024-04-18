using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using ModelLayer.Request_Body;
using ModelLayer.Response;
using System.Data.SqlClient;

namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness Ilabel_bl;

        public LabelController(ILabelBusiness Ilabel_bl)
        {
            this.Ilabel_bl = Ilabel_bl;
        }

        //start

        [HttpPost]
        [Authorize] // Authorize the action
        public async Task<IActionResult> Addlabel(LabelBody labl)
        {
            try
            {
                // Get the UserId of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int userId = int.Parse(userIdClaim.Value);

                // Proceed with adding label using the authenticated UserId
                var details = await Ilabel_bl.Addlabel(labl, userId);

                var response = new ResponseModel<int>
                {
                    Message = "Label Created Successfully",
                    Data = details
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseModel<IEnumerable<label>>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }


        [HttpPut("{labelid}")]
        [Authorize] // Authorize the action
        public async Task<IActionResult> EditbyLabelId(int labelid, LabelBody labl)
        {
            try
            {
                // Get the UserId of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int userId = int.Parse(userIdClaim.Value);
                Console.WriteLine(userId);

                // Proceed with editing label using the authenticated UserId
                var details = await Ilabel_bl.EditbyLabelId(labelid, labl, userId);
                var response = new ResponseModel<int>
                {
                    Message = "Label updated successfully",
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


        [HttpDelete("{LabelId}")]
        [Authorize] // Authorize the action
        public async Task<IActionResult> DeletebyLabelId(int LabelId)
        {
            try
            {
                // Get the UserId of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int userId = int.Parse(userIdClaim.Value);

                // Proceed with deleting label using the authenticated UserId
                var details = await Ilabel_bl.DeletebyLabelId(LabelId, userId);

                if (details > 0)
                {
                    return Ok(new ResponseModel<string>
                    {
                        Message = "Label deleted successfully",
                        Data = null,
                    });
                }
                else
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Label not found",
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


        [HttpGet("{userid}")]
        [Authorize] // Authorize the action
        public async Task<IActionResult> GetbylabelIdAndNotesId(int userid)
        {
            try
            {
                // Get the UserId of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int
                int userId = int.Parse(userIdClaim.Value);

                // Proceed with retrieving labels using the authenticated UserId
                var details = await Ilabel_bl.GetbylabelIdAndNotesId(userId);

                return Ok(new ResponseModel<object>
                {
                    Message = details != null ? "Label retrieved successfully" : "No Label found",
                    Data = details
                }) ;
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return StatusCode(500, new ResponseModel<string>
                    {
                        Success = false,
                        Message = "An error occurred while retrieving Label from the database.",
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





    }
}

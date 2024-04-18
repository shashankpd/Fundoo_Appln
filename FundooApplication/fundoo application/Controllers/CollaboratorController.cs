using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using ModelLayer.Response;
using System.Data.SqlClient;


namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBusiness Icollaborator_bl;

        public CollaboratorController(ICollaboratorBusiness Icollaborator_bl)
        {
            this.Icollaborator_bl = Icollaborator_bl;
        }

        //start

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> Addcollaborator(Collabarator collab)
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

                // Proceed with adding collaborator using the authenticated UserId
                var details = await Icollaborator_bl.Addcollaborator(collab, userId);

                var response = new ResponseModel<int>
                {
                    Message = "Collaborator Created Successfully",
                    Data = details
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseModel<IEnumerable<Collabarator>>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }


        [HttpGet("{collabid}")]
        [Authorize] // Authorize the action
        public async Task<IActionResult> Getbycollabid(int collabid)
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

                // Proceed with retrieving collaborators using the authenticated UserId
                var details = await Icollaborator_bl.Getbycollabid(collabid, userId);

                return Ok(new ResponseModel<IEnumerable<Collabarator>>
                {
                    Message = details != null ? "Collaborator retrieved successfully" : "No Collaborator found",
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
                        Message = "An error occurred while retrieving Collaborator from the database.",
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


        [HttpDelete("{CollabId}")]
        [Authorize] // Authorize the action
        public async Task<IActionResult> Deletebycollabid(int CollabId)
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

                // Proceed with deleting collaborator using the authenticated UserId
                var details = await Icollaborator_bl.Deletebycollabid(CollabId, userId);

                if (details > 0)
                {
                    return Ok(new ResponseModel<string>
                    {
                        Message = "Collaborator deleted successfully",
                        Data = null,
                    });
                }
                else
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Collaborator not found",
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



    }
}

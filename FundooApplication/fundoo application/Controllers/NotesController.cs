using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using ModelLayer.Request_Body;
using ModelLayer.Response;
using Org.BouncyCastle.Utilities;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Security.Claims;


namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness Iusernotes_bl;

        public NotesController(INotesBusiness Iusernotes_bl)
        {
            this.Iusernotes_bl = Iusernotes_bl;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Addnotes(NotesBody notes)
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

                // Call the business logic layer to add the notes
                var details = await Iusernotes_bl.Addnotes(authenticatedUserId, notes);
                var response = new ResponseModel<int>
                {
                    Message = "Note Created Successfully",
                    Data = details
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new ResponseModel<IEnumerable<int>>
                {
                    Success = false,
                    Message = ex.Message,

                };
                return Ok(response);

            }
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            try
            {
                // Get the Userid of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int userId = int.Parse(userIdClaim.Value);

                // Proceed with retrieving notes using the authenticated UserId
                var details = await Iusernotes_bl.GetNotesById(userId);

                return Ok(new ResponseModel<IEnumerable<Notes>>
                {
                    Message = details != null && details.Any() ? "Notes retrieved successfully" : "No notes found",
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
                        Message = "An error occurred while retrieving notes from the database.",
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

        [Authorize]
        [HttpDelete ("ByUserId")]
        public async Task<IActionResult> DeletenotesbyId()
        {
            try
            {
                // Get the Userid of the authenticated user from the claims in the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(500, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int userId = int.Parse(userIdClaim.Value);

                var details = await Iusernotes_bl.DeletenotesbyId(userId);
                if (details>0)
                {
                    return Ok(new ResponseModel<string>
                    {

                        Message = "Note deleted  successfully",
                        Data = null,

                    });
                }
                else
                {
                    return NotFound(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Note not found",
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
        [HttpPut("{Noteid}")]
        public async Task<IActionResult> EditByNoteId(int Noteid, NotesBody note)
        {
            try
            {
                // Get the UserId claim of the authenticated user from the JWT token
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                {
                    // Handle case where UserId claim is missing
                    return StatusCode(401, "UserId claim is missing in the token.");
                }

                // Convert authenticated UserId to int if necessary
                int authenticatedUserId = int.Parse(userIdClaim.Value);

                // Call the business logic layer to edit the note
                var details = await Iusernotes_bl.EditbynoteId(authenticatedUserId, Noteid, note);
                if (details > 0)
                {



                    var response = new ResponseModel<int>
                    {
                        Message = "Note updated successfully",
                        Data = details
                    };

                    return Ok(response);
                }
                var respons = new ResponseModel<int>
                {
                    Success = false,
                    Message = "Note not found",
                    Data = details
                };

                return NotFound(respons);

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




    }
}

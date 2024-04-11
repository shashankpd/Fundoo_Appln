using Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;


namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usernotesController : ControllerBase
    {
        private readonly IUsernotesBusiness Iusernotes_bl;

        public usernotesController(IUsernotesBusiness Iusernotes_bl)
        {
            this.Iusernotes_bl = Iusernotes_bl;
        }

        [HttpPost]
        public async Task<IActionResult> Addnotes(usernotes notes)
        {
            try
            {
                var details = await Iusernotes_bl.Addnotes(notes);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{NoteId}")]
        public async Task<IActionResult> GetNotesById(int id)
        {
            try
            {
                var details = await Iusernotes_bl.GetNotesById(id);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete ("{UserId}")]
        public async Task<IActionResult> DeletenotesbyId(int userid)
        {
            try
            {
                var details = await Iusernotes_bl.DeletenotesbyId( userid);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{NotesId}")]
        public async Task<IActionResult> EditbynoteId(int Noteid, usernotes note)
        {
            try
            {
                var details = await Iusernotes_bl.EditbynoteId( Noteid, note);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


    }
}

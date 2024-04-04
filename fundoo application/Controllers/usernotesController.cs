using Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;

namespace fundoo_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usernotesController : ControllerBase
    {
        private readonly Iusernotes_bl Iusernotes_bl;

        public usernotesController(Iusernotes_bl Iusernotes_bl)
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

        [HttpGet("{id}")]
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

        [HttpDelete ("DeletenotesbyUserId")]
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

        [HttpPut]
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

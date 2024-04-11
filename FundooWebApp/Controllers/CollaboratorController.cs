using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;


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

        [HttpPost("AddCollaborator")]
        public async Task<IActionResult> Addcollaborator(Collabarator collab)
        {
            try
            {
                var details = await Icollaborator_bl.Addcollaborator(collab);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByCollabId")]
        [Authorize]
        public async Task<IActionResult> Getbycollabid(int collabid)
        {
            try
            {
                var details = await Icollaborator_bl.Getbycollabid(collabid);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("DeleteByCollabId")]
        public async Task<IActionResult> Deletebycollabid(int colid)
        {
            try
            {
                var details = await Icollaborator_bl.Deletebycollabid(colid);
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

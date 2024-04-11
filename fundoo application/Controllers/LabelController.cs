using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;

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

        [HttpPost("AddLabel")]
        public async Task<IActionResult> Addlabel(label labl)
        {
            try
            {
                var details = await Ilabel_bl. Addlabel( labl);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
      
        }

        [HttpPut("EditLabel")]
        public async Task<IActionResult> EditbyLabelId(int labelid, label labl)
        {
            try
            {
                var details = await Ilabel_bl.EditbyLabelId(labelid,  labl);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteByLabelId")]
        public async Task<IActionResult> DeletebyLabelId(int lblid)
        {
            try
            {
                var details = await Ilabel_bl.DeletebyLabelId(lblid);
                return Ok(details);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByCollabId")]
        public async Task<IActionResult> GetbylabelIdAndNotesId(int userid)
        {
            try
            {
                var details = await Ilabel_bl.GetbylabelIdAndNotesId(userid);
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

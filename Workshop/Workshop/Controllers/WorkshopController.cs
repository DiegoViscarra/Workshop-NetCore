using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Workshop.Controllers
{
    [Route("[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopServices _workshopServices;
        public WorkshopController(IWorkshopServices workshopServices)
        {
            this._workshopServices = workshopServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkshopModel>> GetWorkshops()
        {
            try
            {
                return Ok(_workshopServices.GetWorkshops());
            }
            catch
            {
                return BadRequest("URL mal escrita");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<WorkshopModel> GetWorkshop([FromRoute] int id)
        {
            try
            {
                return Ok(_workshopServices.GetWorkshop(id));
            }
            catch
            {
                return NotFound("Este workshop no se encuentra registrado");
            }
        }

        [HttpPost]
        public ActionResult<WorkshopModel> PostWorkshop([FromBody] WorkshopModel workshops)
        {
            var newworkshop = _workshopServices.CreateWorkshop(workshops);
            return Created($"/workshop/{newworkshop.Id}", newworkshop);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> PutWorkshop([FromRoute] int id, [FromBody] WorkshopModel workshops)
        {
            try
            {
                var res = _workshopServices.EditWorkshop(id, workshops);
                if (res)
                    return Ok(res);
                else
                    return NotFound("No se encontro el workshop");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteWorkshop([FromRoute] int id)
        {
            try
            {
                var res = _workshopServices.DeleteWorkshop(id);
                if (!res)
                    return StatusCode(StatusCodes.Status500InternalServerError, "No se puede eliminar el workshop");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}/status/{action}")]
        public ActionResult<bool> CancelPostponeWorkshop([FromRoute] int id, [FromRoute] string action)
        {
            try
            {
                var res = _workshopServices.ChangeStatusWorkshop(id, action);
                if (res)
                    return Ok(res);
                else
                    return NotFound("No se encontro el workshop");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

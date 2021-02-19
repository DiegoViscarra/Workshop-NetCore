using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workshop_api.Models;
using workshop_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace workshop_api.Controllers
{
    [Route("[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopServices workshopServices;
        public WorkshopController(IWorkshopServices workshopServices)
        {
            this.workshopServices = workshopServices;
        }
        // GET values
        [HttpGet]
        public ActionResult<IEnumerable<Workshop>> GetWorkshops()
        {
            try
            {
                return Ok(workshopServices.GetWorkshops());
            }
            catch
            {
                return BadRequest("URL mal escrita");
            }
        }

        // GET values/5
        [HttpGet("{id}")]
        public ActionResult<Workshop> GetWorkshop(int id)
        {
            try
            {
                return Ok(workshopServices.GetWorkshop(id));
            }
            catch
            {
                return NotFound("Este workshop no se encuentra registrado");
            }
        }

        // POST values
        [HttpPost]
        public ActionResult<Workshop> PostWorkshop([FromBody] Workshop workshops)
        {
            var newworkshop = workshopServices.CreateWorkshop(workshops);
            return Created($"/workshop/{newworkshop.Id}", newworkshop);
            //Console.WriteLine(workshops);
        }

        // PUT values/5
        [HttpPut("{id}")]
        public ActionResult<bool> PutWorkshop(int id, [FromBody] Workshop workshops)
        {
            try
            {
                var res = workshopServices.EditWorkshop(id, workshops);
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

        // DELETE values/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteWorkshop(int id)
        {
            try
            {
                var res = workshopServices.DeleteWorkshop(id);
                if (!res)
                    return StatusCode(StatusCodes.Status500InternalServerError, "No se puede eliminar el workshop");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT values/5/Status
        [HttpPut("{id:int}/{status}")]
        public ActionResult<bool> CancelPostponeWorkshop(int id, string status)
        {
            try
            {
                var res = workshopServices.ChangeStatusWorkshop(id, status);
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

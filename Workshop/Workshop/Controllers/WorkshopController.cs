﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopLibrary.Exceptions;
using System.Net;

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
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<WorkshopModel> GetWorkshop([FromRoute] int id)
        {
            try
            {
                return Ok(_workshopServices.GetWorkshop(id));
            }
            catch (NotFoundItemException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch
            {
                throw new Exception("Workshop was not found");
            }
        }

        [HttpPost]
        public ActionResult<WorkshopModel> PostWorkshop([FromBody] WorkshopModel workshops)
        {
            try
            {
                var newworkshop = _workshopServices.CreateWorkshop(workshops);
                return Created($"/workshop/{newworkshop.Id}", newworkshop);
            }
            catch (WrongOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> PutWorkshop([FromRoute] int id, [FromBody] WorkshopModel workshops)
        {
            try
            {
                var res = _workshopServices.EditWorkshop(id, workshops);
                return Ok(res);

            }
            catch (NotFoundItemException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteWorkshop([FromRoute] int id)
        {
            try
            {
                var res = _workshopServices.DeleteWorkshop(id);
                return Ok(res);
            }
            catch (NotFoundItemException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
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
                return Ok(res);
            }
            catch (WrongOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

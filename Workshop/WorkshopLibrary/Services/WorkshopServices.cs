using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Models;
using WorkshopLibrary.Exceptions;

namespace Workshop.Services
{
    public class WorkshopServices : IWorkshopServices
    {
        private static List<WorkshopModel> _workshops;
        public WorkshopServices()
        {
            _workshops = new List<WorkshopModel>()
            {
                new WorkshopModel()
                {
                    Id=1,
                    Name="SoftSkills and Rules",
                    Status="Scheduled"
                },
                new WorkshopModel()
                {
                    Id=2,
                    Name="Calidad en Software Comercial",
                    Status="Scheduled"
                },
                new WorkshopModel()
                {
                    Id=3,
                    Name="Bases de Datos",
                    Status="Scheduled"
                }
            };
        }

        public WorkshopModel CreateWorkshop(WorkshopModel workshop)
        {
            if (workshop.Id == null)
            {
                var valid = _workshops.OrderByDescending(a => a.Id).FirstOrDefault();
                if (valid != null)
                {
                    workshop.Id = valid.Id + 1;
                }
                else
                {
                    workshop.Id = 1;
                }
                if(workshop.Status == null || workshop.Status == "Scheduled")
                {
                    workshop.Status = "Scheduled";
                }
                else
                {
                    throw new WrongOperationException("Status has to be Scheduled or Postponed or Cancelled");
                }
                _workshops.Add(workshop);
            }
            else
            {
                _workshops.Add(workshop);
            }
            return workshop;
        }

        public bool DeleteWorkshop(int Id)
        {
            var valid = _workshops.SingleOrDefault(b => b.Id == Id);
            var ans = false;
            if (valid != null)
            {
                _workshops.Remove(valid);
                ans = true;
            }
            else
            {
                throw new NotFoundItemException("The workshop was not found in the DB");
            }
            return ans;
        }

        public bool EditWorkshop(int Id, WorkshopModel workshop)
        {
            var newWorkshop = _workshops.SingleOrDefault(c => c.Id == Id);
            var ans = false;
            if (newWorkshop != null)
            {
                newWorkshop.Id = workshop.Id;
                newWorkshop.Name = workshop.Name;
                newWorkshop.Status = workshop.Status;
                ans = true;
            }
            else
            {
                throw new NotFoundItemException("The workshop was not found in the DB");
            }
            return ans;
        }

        public WorkshopModel GetWorkshop(int Id)
        {
            var getworkshop = _workshops.SingleOrDefault(e => e.Id == Id);
            if (getworkshop != null)
            {
                return getworkshop;
            }
            else
            {
                throw new NotFoundItemException("The workshop was not found in the DB");
            }
        }

        public IEnumerable<WorkshopModel> GetWorkshops()
        {
            return _workshops.OrderBy(d => d.Id);
        }

        public bool ChangeStatusWorkshop(int Id, string Status)
        {
            var newWorkshopStatus = _workshops.SingleOrDefault(c => c.Id == Id);
            var ans = false;
            if (newWorkshopStatus != null)
            {
                if(Status == "postpone")
                {
                    ans = true;
                    newWorkshopStatus.Status = "Postponed";
                }
                else if(Status == "cancel")
                {
                    ans = true;
                    newWorkshopStatus.Status = "Cancelled";
                }
            }
            else
            {
                throw new WrongOperationException("Status has to be postpone or cancel");
            }
            return ans;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Models;

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
                    workshop.Id = 1;
                if(workshop.Status == null)
                {
                    workshop.Status = "Scheduled";
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
            return ans;
        }

        public bool EditWorkshop(int Id, WorkshopModel workshop)
        {
            var newWorkshop = _workshops.SingleOrDefault(c => c.Id == Id);
            if (newWorkshop != null)
            {
                newWorkshop.Id = workshop.Id;
                newWorkshop.Name = workshop.Name;
                newWorkshop.Status = workshop.Status;
                return true;
            }
            else
            {
                throw new Exception();
            }
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
                throw new Exception();
            }
        }

        public IEnumerable<WorkshopModel> GetWorkshops()
        {
            return _workshops.OrderBy(d => d.Id);
        }

        public bool ChangeStatusWorkshop(int Id, string Status)
        {
            var newWorkshopStatus = _workshops.SingleOrDefault(c => c.Id == Id);
            if (newWorkshopStatus != null)
            {
                if(Status == "postpone")
                {
                    newWorkshopStatus.Status = Status;
                }
                else if(Status == "cancel")
                {
                    newWorkshopStatus.Status = Status;
                }
                return true;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}

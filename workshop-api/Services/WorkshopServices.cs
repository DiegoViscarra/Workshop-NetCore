using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workshop_api.Models;

namespace workshop_api.Services
{
    public class WorkshopServices : IWorkshopServices
    {
        private static List<Workshop> workshops;
        public WorkshopServices()
        {
            workshops = new List<Workshop>()
            {
                new Workshop()
                {
                    Id=1,
                    Name="SoftSkills and Rules",
                    Status="Scheduled"
                },
                new Workshop()
                {
                    Id=2,
                    Name="Calidad en Software Comercial",
                    Status="Scheduled"
                },
                new Workshop()
                {
                    Id=3,
                    Name="Bases de Datos",
                    Status="Scheduled"
                }
            };
        }

        public Workshop CreateWorkshop(Workshop workshop)
        {
            if (workshop.Id == null)
            {
                var valid = workshops.OrderByDescending(a => a.Id).FirstOrDefault();
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
                workshops.Add(workshop);
            }
            else
            {
                workshops.Add(workshop);
            }
            return workshop;
        }

        public bool DeleteWorkshop(int Id)
        {
            var valid = workshops.SingleOrDefault(b => b.Id == Id);
            if (valid != null)
            {
                workshops.Remove(valid);
                return true;
            }
            else
                return false;
        }

        public bool EditWorkshop(int Id, Workshop workshop)
        {
            var newWorkshop = workshops.SingleOrDefault(c => c.Id == Id);
            if (newWorkshop != null)
            {
                newWorkshop.Id = workshop.Id;
                newWorkshop.Name = workshop.Name;
                newWorkshop.Status = workshop.Status;
                return true;
            }
            else
                throw new Exception();
        }

        public Workshop GetWorkshop(int Id)
        {
            var getworkshop = workshops.SingleOrDefault(e => e.Id == Id);
            if (getworkshop != null)
                return getworkshop;
            else
                throw new Exception();
        }

        public IEnumerable<Workshop> GetWorkshops()
        {
            return workshops.OrderBy(d => d.Id);
        }

        public bool ChangeStatusWorkshop(int Id, string Status)
        {
            var newWorkshopStatus = workshops.SingleOrDefault(c => c.Id == Id);
            if (newWorkshopStatus != null)
            {
                if(Status == "Postponed")
                    newWorkshopStatus.Status = Status;
                else if(Status == "Cancelled")
                    newWorkshopStatus.Status = Status;
                return true;
            }
            else
                throw new Exception();
        }
    }
}

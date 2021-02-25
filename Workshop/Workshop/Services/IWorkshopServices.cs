using Workshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Services
{
    public interface IWorkshopServices
    {
        IEnumerable<WorkshopModel> GetWorkshops();
        WorkshopModel GetWorkshop(int Id);
        bool EditWorkshop(int Id, WorkshopModel workshop);
        WorkshopModel CreateWorkshop(WorkshopModel workshop);
        bool DeleteWorkshop(int Id);
        bool ChangeStatusWorkshop(int Id, string status);
    }
}

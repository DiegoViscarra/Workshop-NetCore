using workshop_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workshop_api.Services
{
    public interface IWorkshopServices
    {
        IEnumerable<Workshop> GetWorkshops();
        Workshop GetWorkshop(int Id);
        bool EditWorkshop(int Id, Workshop workshop);
        Workshop CreateWorkshop(Workshop workshop);
        bool DeleteWorkshop(int Id);
        bool ChangeStatusWorkshop(int Id, string status);
    }
}

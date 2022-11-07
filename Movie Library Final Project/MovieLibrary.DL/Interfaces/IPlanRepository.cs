using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan?>> GetAllPlans();
        Task<Plan?> GetPlanById(int planId);
        Task<Plan?> AddPlan(Plan plan);
        Task<Plan?> UpdatPlan(Plan plan);
        Task<Plan?> DeletePlan(int planId);
        Task<int> GetPlanPrice(int planId);
    }
}

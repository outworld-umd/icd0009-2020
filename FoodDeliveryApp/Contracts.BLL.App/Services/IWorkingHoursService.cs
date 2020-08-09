using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IWorkingHoursService : IBaseEntityService<WorkingHours>
    {
        public Task<IEnumerable<WorkingHours>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}
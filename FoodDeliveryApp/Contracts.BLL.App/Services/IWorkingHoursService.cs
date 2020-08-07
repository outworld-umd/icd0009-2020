using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IWorkingHoursService : IBaseEntityService<WorkingHours>
    {
        public Task<IEnumerable<WorkingHours>> GetAllByUser(object? userId, bool noTracking = true);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class WorkingHoursService : BaseEntityService<IAppUnitOfWork, IWorkingHoursRepository, IWorkingHoursServiceMapper, DAL.App.DTO.WorkingHours, WorkingHours>, IWorkingHoursService
    {
        public WorkingHoursService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.WorkingHourses, new WorkingHoursServiceMapper())
        {
        }
        
        public async Task<IEnumerable<WorkingHours>> GetAllByUser(object? userId, bool noTracking = true) {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => restaurantIds.Contains(e.Id));
        }
    }
}
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class WorkingHoursService : BaseEntityService<IWorkingHoursRepository, IAppUnitOfWork, DAL.App.DTO.WorkingHours, WorkingHours>, IWorkingHoursService
    {
        public WorkingHoursService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.WorkingHours, WorkingHours>(), unitOfWork.WorkingHourses)
        {
        }
    }
}
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
    }
}
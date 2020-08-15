using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class WorkingHoursMapper: APIMapper<BLL.App.DTO.WorkingHours, WorkingHours>
    {

        public WorkingHours MapWorkingHours(BLL.App.DTO.WorkingHours inObject)
        {
            return Mapper.Map<WorkingHours>(inObject);
        }
    }
}
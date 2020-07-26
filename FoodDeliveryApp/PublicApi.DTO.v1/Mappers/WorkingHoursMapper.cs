using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class WorkingHoursMapper: BaseAPIMapper<BLL.App.DTO.WorkingHours, WorkingHours>
    {
        public WorkingHoursMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.WorkingHours, WorkingHours>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public WorkingHours MapWorkingHours(BLL.App.DTO.WorkingHours inObject)
        {
            return Mapper.Map<WorkingHours>(inObject);
        }
    }
}
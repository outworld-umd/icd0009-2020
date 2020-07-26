using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class WorkingHoursServiceMapper: BaseBLLMapper<DALAppDTO.WorkingHours, BLLAppDTO.WorkingHours>, IWorkingHoursServiceMapper
    {
        public WorkingHoursServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.WorkingHours, BLLAppDTO.WorkingHours>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.WorkingHours, DALAppDTO.WorkingHours>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemServiceMapper: BaseBLLMapper<DALAppDTO.Item, BLLAppDTO.Item>, IItemServiceMapper
    {
        public ItemServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.NutritionInfo, DALAppDTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.NutritionInfo, BLLAppDTO.NutritionInfo>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemOption, DALAppDTO.ItemOption>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemOption, BLLAppDTO.ItemOption>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemChoice, DALAppDTO.ItemChoice>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
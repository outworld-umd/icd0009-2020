using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemChoiceServiceMapper: BaseBLLMapper<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>, IItemChoiceServiceMapper
    {
        public ItemChoiceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemChoice, DALAppDTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemOption, DALAppDTO.ItemOption>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemOption, BLLAppDTO.ItemOption>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
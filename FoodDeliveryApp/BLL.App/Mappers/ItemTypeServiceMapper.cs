using AutoMapper;
using AutoMapper.Configuration;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemTypeServiceMapper: BLLMapper<DALAppDTO.ItemType, BLLAppDTO.ItemType>, IItemTypeServiceMapper
    {
    }
}
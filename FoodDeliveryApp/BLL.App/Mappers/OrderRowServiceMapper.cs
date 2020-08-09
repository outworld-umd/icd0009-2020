using AutoMapper;
using BLL.App.Mappers.Old;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderRowServiceMapper: BLLMapper<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>, IOrderRowServiceMapper
    {
        
        public BLLAppDTO.OrderRow MapOrderRow(DALAppDTO.OrderRow inObject)
        {
            var orderRow = Mapper.Map<BLLAppDTO.OrderRow>(inObject);
            if (orderRow.Name!.Equals(null)) {
                orderRow.Name = inObject.Item!.Name;
            }
            return orderRow;
        }
    }
}
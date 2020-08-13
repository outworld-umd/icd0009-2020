using AutoMapper;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using ee.itcollege.anguzo.DTO.Identity;
namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            
            MapperConfigurationExpression.CreateMap<ee.itcollege.anguzo.Domain.Identity.AppUser, ee.itcollege.anguzo.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Domain.App.Address, DAL.App.DTO.Address>();


            MapperConfigurationExpression.CreateMap<ee.itcollege.anguzo.DTO.Identity.AppUser, ee.itcollege.anguzo.Domain.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Address, Domain.App.Address>();


            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
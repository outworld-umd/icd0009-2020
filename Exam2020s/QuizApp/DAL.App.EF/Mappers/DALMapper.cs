using AutoMapper;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using DALAppDTO = DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper()
        {
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Answer, DALAppDTO.Answer>();
            MapperConfigurationExpression.CreateMap<Choice, DALAppDTO.Choice>();
            MapperConfigurationExpression.CreateMap<Question, DALAppDTO.Question>();
            MapperConfigurationExpression.CreateMap<Quiz, DALAppDTO.Quiz>();
            MapperConfigurationExpression.CreateMap<QuizSession, DALAppDTO.QuizSession>();


            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, Domain.App.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Answer, Answer>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Choice, Choice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Question, Question>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Quiz, Quiz>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.QuizSession, QuizSession>();


            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
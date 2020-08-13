using AutoMapper;
using DALAppDTO = DAL.App.DTO;

namespace PublicApi.DTO.Mappers.Base
{
    public class APIMapper<TLeftObject, TRightObject> : BaseAPIMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public APIMapper()
        {
            MapperConfigurationExpression.CreateMap<Quiz, DALAppDTO.Quiz>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Quiz, Quiz>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Quiz, QuizView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Quiz, QuizDisplay>();
            
            MapperConfigurationExpression.CreateMap<Question, DALAppDTO.Question>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Question, Question>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Question, QuestionView>();
            
            MapperConfigurationExpression.CreateMap<Choice, DALAppDTO.Choice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Choice, Choice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Choice, ChoiceView>();
            
            MapperConfigurationExpression.CreateMap<Answer, DALAppDTO.Answer>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Answer, Answer>();
            
            MapperConfigurationExpression.CreateMap<QuizSession, DALAppDTO.QuizSession>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.QuizSession, QuizSession>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.QuizSession, QuizSessionView>();



            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
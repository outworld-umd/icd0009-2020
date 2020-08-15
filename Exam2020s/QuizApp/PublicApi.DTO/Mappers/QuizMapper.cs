using System.Linq;
using AutoMapper.Internal;
using PublicApi.DTO.Mappers.Base;

namespace PublicApi.DTO.Mappers
{
    public class QuizMapper : APIMapper<DAL.App.DTO.Quiz, Quiz>
    {
        public QuizView MapView(DAL.App.DTO.Quiz quiz)
        {
            return Mapper.Map<QuizView>(quiz);
        }

        public QuizDisplay MapDisplay(DAL.App.DTO.Quiz quiz)
        {
            return Mapper.Map<QuizDisplay>(quiz);
        }
    }
}
using System;
using System.Linq;
using AutoMapper.Internal;
using Newtonsoft.Json;
using PublicApi.DTO.Mappers.Base;

namespace PublicApi.DTO.Mappers
{
    public class QuizSessionMapper : APIMapper<DAL.App.DTO.QuizSession, QuizSession>
    {
        public QuizSessionView MapView(DAL.App.DTO.QuizSession quizSession)
        {
            var entity = Mapper.Map<QuizSessionView>(quizSession);
            return Mapper.Map<QuizSessionView>(quizSession);
        }
    }
}
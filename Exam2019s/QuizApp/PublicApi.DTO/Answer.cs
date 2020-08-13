using System;

namespace PublicApi.DTO
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid ChoiceId { get; set; }
        
        public Guid QuizSessionId { get; set; }
        
        public bool? IsCorrect { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
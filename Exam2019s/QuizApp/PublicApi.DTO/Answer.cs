using System;

namespace PublicApi.DTO
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid ChoiceId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
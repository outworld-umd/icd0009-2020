using System;
using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class QuizSession
    {
        public Guid Id { get; set; } = default!;
        public Guid QuizId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public QuizView? Quiz { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }

    public class QuizSessionView
    {
        public Guid Id { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public QuizDisplay? Quiz { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Domain.App.Enums;

namespace PublicApi.DTO
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public QuizType QuizType { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }

    public class QuizView
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public QuizType QuizType { get; set; }
        public ICollection<QuestionView>? Questions { get; set; }
        public int TimesTaken { get; set; }
    }

    public class QuizDisplay
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public QuizType QuizType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TimesTaken { get; set; }
    }
}
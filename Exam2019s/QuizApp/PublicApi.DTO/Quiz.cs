using System;
using System.Collections.Generic;
using Type = Domain.App.Enums.Type;

namespace PublicApi.DTO
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Type Type { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }

    public class QuizView
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Type Type { get; set; }
        public ICollection<QuestionView>? Questions { get; set; }
    }

    public class QuizDisplay
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Type Type { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<Choice>? Choices { get; set; }
        public Guid? CorrectChoiceId { get; set; }
    }

    public class QuestionView
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<ChoiceView>? Choices { get; set; }
        public Guid QuizId { get; set; }
        public int TimesAnswered { get; set; }
        public Guid? CorrectChoiceId { get; set; }
    }
}
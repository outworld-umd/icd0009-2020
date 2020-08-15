using System;
using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class Choice
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;
    }

    public class ChoiceView
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;
        public Guid QuestionId { get; set; }
        public int TimesAnswered { get; set; }
    }
}
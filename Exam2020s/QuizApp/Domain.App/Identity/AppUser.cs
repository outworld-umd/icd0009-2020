using System.Collections.Generic;
using BaseIdentity = ee.itcollege.anguzo.Domain.Identity;

namespace Domain.App.Identity
{
    public class AppUser : BaseIdentity.AppUser
    {
        public ICollection<Quiz>? Quizzes { get; set; }
        public ICollection<QuizSession>? QuizSessions { get; set; }
    }
}
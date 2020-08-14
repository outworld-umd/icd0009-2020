using System.Collections.Generic;
using System.ComponentModel;
using BaseIdentity = ee.itcollege.anguzo.Domain.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : BaseIdentity.AppUser
    {
        [DisplayName("Quizzes")] public ICollection<Quiz>? Quizzes { get; set; }
        [DisplayName("QuizSessions")] public ICollection<QuizSession>? QuizSessions { get; set; }
    }
}
using System.Collections.Generic;
using BaseIdentity = ee.itcollege.anguzo.Domain.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : BaseIdentity.AppUser
    {
        public ICollection<Quiz>? Quizzes { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
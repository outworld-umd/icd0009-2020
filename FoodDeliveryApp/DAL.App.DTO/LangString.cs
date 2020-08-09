using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using Domain.App;
using ee.itcollege.anguzo.Domain.Base;
namespace DAL.App.DTO
{
    public class LangString : IDomainEntityIdMetadata
    {
        public ICollection<Translation>? Translations { get; set; }
        [InverseProperty(nameof(Category.Name))]
        public ICollection<Category>? CategoryNames { get; set; }
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
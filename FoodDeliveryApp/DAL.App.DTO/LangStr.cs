using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class LangStr : IDomainEntityIdMetadata
    {
        public ICollection<Translation>? Translations { get; set; }
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
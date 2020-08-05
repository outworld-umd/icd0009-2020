using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Translation = BLL.App.DTO.Translation;

namespace BLL.App.DTO
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
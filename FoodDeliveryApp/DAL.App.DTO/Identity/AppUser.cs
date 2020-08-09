using System;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Basic;

namespace DAL.App.DTO.Identity {

    public class AppUser : AppUser<Guid>
    {
    }
    
    public class AppUser<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>

    {
        public TKey Id { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        public virtual string Phone { get; set; } = default!;

        public virtual string FullName => FirstName + " " + LastName;
    }
}
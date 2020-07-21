using System;
using Contracts.Domain;
using Contracts.Domain.Basic;

namespace DAL.App.DTO.Identity {

    public class AppUser : AppUser<Guid> {
        
    }

    public class AppUser<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey> {
        public TKey Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string FullName => FirstName + " " + LastName;

    }

}
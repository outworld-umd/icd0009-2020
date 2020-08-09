using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;
using Contracts.Domain.Base.Basic;
namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }
    
    public class AppUser<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>

    {
        public TKey Id { get; set; } = default!;
        
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string FullName => FirstName + " " + LastName;
    }
}
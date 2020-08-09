using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;
namespace Domain.App
{
    
    public class RestaurantCategory : RestaurantCategory<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class RestaurantCategory<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey> 
    {
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}
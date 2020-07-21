using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

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
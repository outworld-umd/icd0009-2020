using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;
namespace Domain.App
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class OrderItemChoice<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey> 
    {
        public TKey OrderRowId { get; set; } = default!;
        public OrderRow? OrderRow { get; set; }

        public Guid? ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }
        
        public string? Name { get; set; } = default!;
        
        [Range(1, 20)] [Required] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal Cost { get; set; }
    }
}
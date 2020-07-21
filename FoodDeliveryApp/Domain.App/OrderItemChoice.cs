using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class OrderItemChoice<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey> 
    {
        public Guid? OrderRowId { get; set; } // ZDES BIL 0!!!!!!!!!!!!!!!
        public OrderRow? OrderRow { get; set; }

        public TKey ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }
        
        [Range(1, 20)] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal Cost { get; set; }
    }
}
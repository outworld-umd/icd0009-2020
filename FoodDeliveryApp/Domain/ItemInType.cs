using System;
using DAL.Base;

namespace Domain
{
    public class ItemInType : DomainEntityBaseMetadata
    {
        public Guid ItemTypeId { get; set; }
        public ItemType? ItemType { get; set; }
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
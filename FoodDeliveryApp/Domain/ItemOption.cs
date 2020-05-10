﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class ItemOption : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public bool IsRequired { get; set; } = default!;
        public bool IsSingle { get; set; } = default!;


        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        public ICollection<ItemChoice>? ItemChoices { get; set; }
    }
}
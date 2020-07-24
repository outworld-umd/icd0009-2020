﻿using System;

namespace Contracts.Domain.Basic
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
    }

    public interface IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
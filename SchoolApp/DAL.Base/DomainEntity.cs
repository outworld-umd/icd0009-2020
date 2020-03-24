using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity :  DomainEntity<Guid>
    {
    }

    public abstract class DomainEntity<TKey> :  DomainEntityMetadata, IDomainEntity<TKey> 
        where TKey : struct, IComparable
    {
        public virtual TKey Id { get; set; }
    }

}
using System;
using Microsoft.AspNetCore.Identity;

namespace ee.itcollege.anguzo.Domain.Identity
{
    public class AppRole : AppRole<Guid>
    {
    }

    public class AppRole<TKey> : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
using System;

namespace BLL.App.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }

    public class AppUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string FullName => FirstName + " " + LastName;
    }
}
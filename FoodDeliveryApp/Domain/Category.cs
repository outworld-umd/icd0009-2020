using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Category : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        
    }
}
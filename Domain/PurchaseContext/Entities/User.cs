using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.PurchaseContext.Entities
{
    public class User : IdentityUser
    {
        [Column("Name")]
        public string Name { get; set; } = string.Empty;
        
        [Column("CPF")]
        public string CPF { get; set; } = string.Empty;
    }
}
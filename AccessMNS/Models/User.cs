using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessMNS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("UserRole")]
        public int Id_Role { get; set; }
        [ForeignKey("UserStatus")]
        public int Id_Status { get; set; }
        [ForeignKey("UserGroup")]
        public int? Id_Group { get; set; }
        public string? Avatar { get; set; }

        public virtual Role UserRole { get; set; }
        public virtual Status UserStatus { get; set; }
        public virtual Group? UserGroup { get; set; }
    }
}

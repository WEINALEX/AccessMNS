using System.ComponentModel.DataAnnotations;

namespace AccessMNS.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<User>? RoleUser { get; set; }
    }
}

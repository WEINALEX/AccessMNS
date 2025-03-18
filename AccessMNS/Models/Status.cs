using System.ComponentModel.DataAnnotations;

namespace AccessMNS.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<User>? StatusUser { get; set; }
    }
}

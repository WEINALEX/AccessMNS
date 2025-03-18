using System.ComponentModel.DataAnnotations;

namespace AccessMNS.Models
{
    public class ChannelType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

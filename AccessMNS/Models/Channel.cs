using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessMNS.Models
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("ChannelType")]
        public int Id_Channel_Type { get; set; }
        public string? Image_channel { get; set; }

        public virtual ChannelType ChannelType { get; set; }
    }
}

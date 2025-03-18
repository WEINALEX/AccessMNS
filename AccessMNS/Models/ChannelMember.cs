using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessMNS.Models
{
    public class ChannelMember
    {
        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Channels")]
        public int IdChannel { get; set; }

        public virtual User User { get; set; }
        public virtual List<Channel> Channels { get; set; }
    }
}

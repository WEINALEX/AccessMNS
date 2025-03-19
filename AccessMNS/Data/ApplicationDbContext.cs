using AccessMNS.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessMNS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<ChannelType> ChannelType { get; set; }
        public DbSet<ChannelMember> ChannelMember { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("user")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<User>()
                .HasOne(ts => ts.UserRole)
                .WithMany(t => t.RoleUser)
                .HasForeignKey(ts => ts.Id_Role);

            modelBuilder.Entity<User>()
                .HasOne(ts => ts.UserStatus)
                .WithMany(t => t.StatusUser)
                .HasForeignKey(ts => ts.Id_Status);

            modelBuilder.Entity<User>()
                .HasOne(ts => ts.UserGroup)
                .WithMany(t => t.GroupUser)
                .HasForeignKey(ts => ts.Id_Group);

            modelBuilder.Entity<Role>()
                .ToTable("role")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Status>()
                .ToTable("status")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Group>()
                .ToTable("group_name")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Channel>()
                .ToTable("channel")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Channel>()
                .HasOne(ts => ts.ChannelType)
                .WithMany(t => t.Channels)
                .HasForeignKey(ts => ts.Id_Channel_Type);

            modelBuilder.Entity<ChannelType>()
                .ToTable("channel_type")
                .Property(ts => ts.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<ChannelMember>()
                .ToTable("channel_member")
                .HasKey(e => new { e.IdUser, e.IdChannel });
        }
    }
}

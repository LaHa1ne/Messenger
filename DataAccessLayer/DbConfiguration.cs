using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {

            entity.ToTable("users");

            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.Email, "Email_UNIQUE")
               .IsUnique();
            entity.HasIndex(e => e.Nickname, "Nickname_UNIQUE")
                .IsUnique();
            entity.HasIndex(e => e.UserId, "UserId_UNIQUE")
                .IsUnique();

            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Nickname).HasMaxLength(16);
            entity.Property(e => e.Password).HasMaxLength(120);

            entity.Property(e => e.Nickname).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();


            entity
           .HasMany(c => c.Friends)
           .WithMany(s => s.FriendsNavigation)
           .UsingEntity(j => j.ToTable("Friends"));

            entity
                .HasMany(c => c.Senders)
                .WithMany(s => s.SendersNavigation)
                .UsingEntity(j => j.ToTable("Senders"));
        }
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {

            entity.ToTable("messages");

            entity.HasKey(e => e.MessageId);

            entity.HasIndex(e => new { e.ChatId, e.Date }, "ChatMessagesIndex");
            entity.HasIndex(e => e.MessageId, "MessageIndex").IsUnique();

            entity.Property(e => e.Text).HasMaxLength(200);
            //entity.Property(e => e.Date).HasDefaultValueSql("DATETIME('now')");
            //entity.Property(e => e.Date).HasDefaultValueSql("NOW()");
            entity.Property(e => e.IsSystem).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ChatId).IsRequired();
            entity.Property(e => e.Text).IsRequired();
        }
    }

    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> entity)
        {
            entity.ToTable("chats");

            entity.HasKey(e => e.ChatId);

            entity.HasIndex(e => e.ChatId, "ChatId_UNIQUE")
                .IsUnique();

            entity.Property(e => e.Name).HasMaxLength(40);
            entity.Property(e => e.Type).HasColumnType("enum('Personal','Group')");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.ChatCreatorId).IsRequired();

            entity
                .HasMany(c => c.ChatMembers)
                .WithMany(u => u.Chats)
                .UsingEntity(j => j.ToTable("ChatMembers"));

            entity.HasOne(d => d.ChatCreator)
                   .WithMany(p => p.CreatedChats)
                   .HasForeignKey(d => d.ChatCreatorId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("Creator");
        }
    }
}

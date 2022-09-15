using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Shared.Extensions;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Shared.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }  // review logical
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<Recommendation> Recommendations { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(p => p.Age).IsRequired();
        builder.Entity<User>().Property(p => p.Image).IsRequired();
        builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(200);
        builder.Entity<User>().Property(p => p.Specialist).IsRequired().HasMaxLength(70);
        builder.Entity<User>().Property(p => p.Recommendation).IsRequired();
        builder.Entity<User>().Property(p => p.WorkPlace).IsRequired();
        builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(80);
        builder.Entity<User>().Property(p => p.Biography).IsRequired();
        
        // Relationships
        builder.Entity<User>()
            .HasMany(p => p.Forums)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Chats)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.Entity<User>()
            .HasMany(p => p.Recommendations)
            .WithOne(p => p.userRecommendation)
            .HasForeignKey(p => p.recommendationUserId)
            .HasForeignKey(p => p.recommendedUserId);

        builder.Entity<User>()
            .HasMany(p => p.Notifications)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        ///////////////////////////////////////////
        builder.Entity<Comment>().ToTable("Comments");
        builder.Entity<Comment>().HasKey(p => p.Id);
        builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Comment>().Property(p => p.Date).IsRequired();
        builder.Entity<Comment>().Property(p => p.Content).HasMaxLength(500);
        ///////////////////////////////////////////
        builder.Entity<Forum>().ToTable("Forums");
        builder.Entity<Forum>().HasKey(p => p.Id);
        builder.Entity<Forum>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Forum>().Property(p => p.Date).IsRequired();
        builder.Entity<Forum>().Property(p => p.Content).HasMaxLength(500);
        builder.Entity<Forum>().Property(p => p.Title).HasMaxLength(200);
        
        builder.Entity<Forum>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.Forum)
            .HasForeignKey(p => p.ForumId);
        
        builder.Entity<Forum>()
            .HasMany(p => p.Ratings)
            .WithOne(p => p.Forum)
            .HasForeignKey(p => p.ForumId);
        
        /////////////////////////////////////////////////////
        builder.Entity<Rating>().ToTable("Ratings");
        builder.Entity<Rating>().HasKey(p => p.Id);
        builder.Entity<Rating>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        

        ////////////////////////////////////////////////////*/

        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(p => p.Title).IsRequired().HasMaxLength(30);
        builder.Entity<Notification>().Property(p => p.UserId).IsRequired();
        builder.Entity<Notification>().Property(p => p.ReferencesToUserId).IsRequired();
        ///////////////////////////////////////////////////////
        builder.Entity<Chat>().ToTable("Chats");
        builder.Entity<Chat>().HasKey(p => p.Id);
        builder.Entity<Chat>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Chat>().Property(p => p.UserId).IsRequired();
        builder.Entity<Chat>().Property(p => p.UserDestinyId).IsRequired();
        builder.Entity<Chat>()
            .HasMany(p => p.Messages)
            .WithOne(p => p.Chat)
            .HasForeignKey(p => p.ChatId);
        /////////////////////////////////////////////////////
        builder.Entity<Message>().ToTable("Messages");
        builder.Entity<Message>().HasKey(p => p.Id);
        builder.Entity<Message>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Message>().Property(p => p.Content).IsRequired();
        builder.Entity<Message>().Property(p => p.UserId).IsRequired();
        builder.Entity<Message>().Property(p => p.UserDestinyId).IsRequired();
        builder.Entity<Message>().Property(p => p.ChatId).IsRequired();
        
        //////////////////////////////////////////////
        builder.Entity<Recommendation>().ToTable("Recommendations");
        builder.Entity<Recommendation>().HasKey(p => p.Id);
        builder.Entity<Recommendation>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Recommendation>().Property(p => p.recommendationUserId).IsRequired();
        builder.Entity<Recommendation>().Property(p => p.recommendedUserId).IsRequired();

        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}
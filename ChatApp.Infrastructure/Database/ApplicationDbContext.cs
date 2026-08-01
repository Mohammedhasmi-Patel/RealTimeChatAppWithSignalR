using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<ApplicationUser> ApplicationUser { get; set; } = null!;
    public DbSet<ApplicationRole> ApplicationRole { get; set; } = null!;

    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
}

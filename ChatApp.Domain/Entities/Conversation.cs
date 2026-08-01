
using ChatApp.Domain.Enum;

namespace ChatApp.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }

        public ConversationTypeEnum Type { get; set; }

        public string? Title { get; set; }

        public string? Avatar { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation Properties
        public ICollection<ConversationParticipant> Members { get; set; } = new List<ConversationParticipant>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}

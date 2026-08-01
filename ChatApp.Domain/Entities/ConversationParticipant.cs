
using ChatApp.Domain.Enum;

namespace ChatApp.Domain.Entities
{
    public class ConversationParticipant
    {
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }

        public ConversationParticipantRoleEnum Role { get; set; } = ConversationParticipantRoleEnum.User;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LeftAt { get; set; }

        public Guid? LastReadMessageId { get; set; }


        // Navigation Properties
        public Conversation Conversation { get; set; } = null!;


        public Message? LastReadMessage { get; set; }

    }
}


using ChatApp.Domain.Enum;

namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }

        public Guid SenderId { get; set; }


        public MessageTypeEnum Type { get; set; }

        public string? Content { get; set; }


        public string? AttachmentUrl { get; set; }

        public string? AttachmentName { get; set; }

        public int? AttachmentSize { get; set; }

        public string? AttachmentMimeType { get; set; }


        public Guid? ReplyToMessageId { get; set; }


        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public DateTime? EditedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        // Navigation Properties
        public Conversation Conversation { get; set; } = null!;


        public Message? ReplyToMessage { get; set; }

        public ICollection<Message> Replies { get; set; } = new List<Message>();

    }
}

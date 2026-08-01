
using ChatApp.Domain.Enum;

namespace ChatApp.Domain.Entities
{
    public class FriendRequest
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public FriendRequestStatusEnum Status { get; set; } = FriendRequestStatusEnum.Pending;

        public DateTime? ResponseAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Navigation Properties

    }
}

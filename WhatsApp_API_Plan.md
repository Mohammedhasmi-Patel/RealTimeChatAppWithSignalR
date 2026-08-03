# WhatsApp-Style Chat Application API Plan

## 1. Domain entities already identified

The current backend domain already contains the following core entities:

- Conversation
  - represents a chat room or DM thread
  - fields: Id, Type, Title, Avatar, CreatedBy, CreatedAt, UpdatedAt
  - navigation: Members, Messages

- ConversationParticipant
  - joins a user to a conversation
  - fields: Id, ConversationId, UserId, Role, JoinedAt, LeftAt, LastReadMessageId
  - navigation: Conversation, LastReadMessage

- Message
  - stores chat messages and attachments
  - fields: Id, ConversationId, SenderId, Type, Content, AttachmentUrl, AttachmentName, AttachmentSize, AttachmentMimeType, ReplyToMessageId, SentAt, EditedAt, DeletedAt
  - navigation: Conversation, ReplyToMessage, Replies

- Friend
  - stores accepted contact relationships
  - fields: Id, UserId, FriendId, CreatedAt

- FriendRequest
  - stores pending/accepted/rejected friend requests
  - fields: Id, SenderId, ReceiverId, Status, ResponseAt, CreatedAt

- ErrorLog
  - for app-level error capture and diagnostics

## 2. API priority after authentication

Once auth is completed, the backend should expose the following API groups in order.

### A. Profile and user management

1. GET /api/users/me
   - return current logged-in user profile

2. PUT /api/users/me
   - update profile info, avatar, status, display values

3. GET /api/users/search
   - search users by name/email to start a chat or add friend

4. GET /api/users/{userId}
   - view another user public profile

### B. Friend and contact APIs

5. POST /api/friends/requests
   - send a friend request

6. GET /api/friends/requests
   - list incoming and outgoing friend requests

7. PATCH /api/friends/requests/{requestId}/accept
   - accept a friend request

8. PATCH /api/friends/requests/{requestId}/reject
   - reject a friend request

9. GET /api/friends
   - list accepted friends for the current user

10. DELETE /api/friends/{friendId}
    - remove friend connection

### C. Conversation APIs

11. GET /api/conversations
    - list all conversations for the current user

12. POST /api/conversations
    - create a new one-to-one or group conversation

13. GET /api/conversations/{conversationId}
    - get conversation details and metadata

14. PATCH /api/conversations/{conversationId}
    - update group title/avatar or conversation metadata

15. POST /api/conversations/{conversationId}/participants
    - add user(s) into a group conversation

16. DELETE /api/conversations/{conversationId}/participants/{userId}
    - remove participant from a conversation

17. PATCH /api/conversations/{conversationId}/leave
    - allow a user to leave a conversation

### D. Message APIs

18. GET /api/conversations/{conversationId}/messages
    - get paginated message history

19. POST /api/conversations/{conversationId}/messages
    - send a text message

20. POST /api/conversations/{conversationId}/messages/attachment
    - send image/file attachment message

21. GET /api/messages/{messageId}
    - get details of a specific message

22. PUT /api/messages/{messageId}
    - edit message content

23. DELETE /api/messages/{messageId}
    - soft-delete a message

24. POST /api/messages/{messageId}/reply
    - reply to an existing message

### E. Read-state and presence APIs

25. PATCH /api/conversations/{conversationId}/read
    - mark conversation as read for the current user

26. GET /api/conversations/{conversationId}/unread-count
    - count unread messages for the user

27. PATCH /api/users/me/status
    - update online/active presence state

### F. File and media API

28. POST /api/uploads/avatar
    - upload avatar image

29. POST /api/uploads/message
    - upload message attachment files

### G. Admin or diagnostics API

30. GET /api/error-logs
    - internal monitoring endpoint for debugging server errors

31. GET /api/error-logs/{id}
    - inspect a specific logged exception

## 3. Recommended implementation order

1. User profile APIs
2. Friends and friend request APIs
3. Conversation create/list/detail APIs
4. Conversation participant APIs
5. Message send/list/edit/delete APIs
6. Read-state and presence APIs
7. File upload APIs
8. Diagnostics endpoints

## 4. Suggested controller grouping

- AuthController (already present)
- UserController
- FriendController
- ConversationController
- MessageController
- UploadController
- ErrorLogController

## 5. Suggested DTO structure

Create request/response DTOs for each API group:

- user profile DTOs
- friend request DTOs
- friend list DTOs
- conversation DTOs
- participant DTOs
- message DTOs
- upload DTOs
- pagination/query DTOs

## 6. Important backend rules for this app

- All message operations must be scoped to the current user.
- A participant must exist in the conversation before sending a message.
- Group conversation updates should only be allowed for admins or creators.
- Message deletion should be soft-delete based on DeletedAt.
- Read-state should be tracked by the last read message id in participants.
- Attachments should be stored via file storage after upload validation.

## 7. Best next step

Start with the following minimal MVP sequence:

1. user profile
2. friend request flow
3. conversation creation
4. message posting
5. conversation message listing

This gives the WhatsApp-style chat experience with the smallest useful working surface.

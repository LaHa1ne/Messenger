﻿using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messenger2.DataAccessLayer.Interfaces;
using messenger2.DataLayer.DTO;
using Microsoft.EntityFrameworkCore;
using DataLayer.Enums;

namespace messenger2.DataAccessLayer.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<ChatDTO> LoadChat(int ChatId, int num_messages_to_load, int UserId)
        {
            var chat = await _db.Chats.Include(c => c.ChatMembers).Include(c => c.ChatMessages.Where(m => m.IsDeleted != true).OrderByDescending(m => m.Date).Take(num_messages_to_load+1)).FirstOrDefaultAsync(c => c.ChatId == ChatId);
            var chatDTO = new ChatDTO(chat.ChatMessages.Count(), chat.ChatMembers.Count()) { 
                ChatId = chat.ChatId, 
                Name = chat.Type == ChatType.Personal ? chat.ChatMembers.FirstOrDefault(u => u.UserId != UserId).Nickname : chat.Name, 
                HasMoreMessages = chat.ChatMessages.Count() > num_messages_to_load 
            };

            foreach (var message in chat.ChatMessages.Reverse().TakeLast(num_messages_to_load))
            {
                chatDTO.Messages.Add(new MessageBriefInfoDTO()
                {
                    MessageId = message.MessageId,
                    Nickname = message.MessageCreator != null ? message.MessageCreator.Nickname : "",
                    Text = message.Text,
                    Date = message.Date,
                    IsSystem = message.IsSystem,
                    IsMine = message.MessageCreatorId == UserId ? true : false
                });
            }

            foreach (var member in chat.ChatMembers)
            {
                chatDTO.Members.Add(new UserBriefInfoDTO(member.UserId, member.Nickname));
            }

            return chatDTO;
        }

        public async Task<ChatDTO> LoadPersonalChatWithFriend(User Friend, int num_messages_to_load, User User)
        {
            var chat = await _db.Chats.Include(c => c.ChatMembers).Include(c => c.ChatMessages.Where(m => m.IsDeleted != true).OrderByDescending(m => m.Date).Take(num_messages_to_load + 1)).FirstOrDefaultAsync(c => c.ChatMembers.Contains(Friend) && c.ChatMembers.Contains(User));
            if (chat == null)
            {
                chat = new Chat()
                {
                    Name = "",
                    Type = ChatType.Personal,
                    ChatCreator = User,
                    ChatMembers = new HashSet<User>(2) { User, Friend }
                };
                await Create(chat);
            }

            var chatDTO = new ChatDTO(chat.ChatMessages.Count(), chat.ChatMembers.Count())
            {
                ChatId = chat.ChatId,
                Name = chat.ChatMembers.FirstOrDefault(u => u.UserId != User.UserId).Nickname,
                HasMoreMessages = chat.ChatMessages.Count() > num_messages_to_load
            };

            foreach (var message in chat.ChatMessages.Reverse().TakeLast(num_messages_to_load))
            {
                chatDTO.Messages.Add(new MessageBriefInfoDTO()
                {
                    MessageId = message.MessageId,
                    Nickname = message.MessageCreator != null ? message.MessageCreator.Nickname : "",
                    Text = message.Text,
                    Date = message.Date,
                    IsSystem = message.IsSystem,
                    IsMine = message.MessageCreatorId == User.UserId ? true : false
                });
            }

            foreach (var member in chat.ChatMembers)
            {
                chatDTO.Members.Add(new UserBriefInfoDTO(member.UserId, member.Nickname));
            }

            return chatDTO;
        }

        public async Task<MessageListDTO> LoadMoreMessages(int ChatId, int num_messages_to_load, int FirstMessageId, int UserId)
        {
            var chat = await _db.Chats.Include(c => c.ChatMembers).Include(c => c.ChatMessages.Where(m => m.IsDeleted != true && m.MessageId < FirstMessageId).OrderByDescending(m => m.Date).Take(num_messages_to_load+1)).FirstOrDefaultAsync(c => c.ChatId == ChatId);
            var messageListDTO = new MessageListDTO(chat.ChatMessages.Count()) { HasMoreMessages = chat.ChatMessages.Count() > num_messages_to_load };

            foreach (var message in chat.ChatMessages.Take(num_messages_to_load))
            {
                messageListDTO.Messages.Add(new MessageBriefInfoDTO()
                {
                    MessageId = message.MessageId,
                    Nickname = message.MessageCreator != null ? message.MessageCreator.Nickname : "",
                    Text = message.Text,
                    Date = message.Date,
                    IsSystem = message.IsSystem,
                    IsMine = message.MessageCreatorId == UserId ? true : false
                });
            }
            return messageListDTO;
        }

        public async Task<bool> AddMessage(int ChatId, Message message)
        {
            Chat chat = await _db.Chats.Include(c => c.ChatMessages).FirstOrDefaultAsync(c => c.ChatId == ChatId);
            chat.ChatMessages.Add(message);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ChatBriefInfoDTO>> GetPersonalChats(int UserId)
        {
            var user = await _db.Users.Include(u => u.Chats.Where(c => c.Type == ChatType.Personal && c.ChatMessages.Count > 0)).ThenInclude(c => c.ChatMembers.Where(u => u.UserId != UserId)).Include(u => u.Chats.Where(c => c.Type == ChatType.Personal && c.ChatMessages.Count > 0)).ThenInclude(c => c.ChatMessages.Where(m => m.IsDeleted != true).OrderByDescending(m => m.Date).Take(1)).FirstOrDefaultAsync(u => u.UserId == UserId);

            var chatList = new List<ChatBriefInfoDTO>(user.Chats.Count(c => c.Type == ChatType.Personal && c.ChatMessages.Count > 0));
            foreach (var chat in user.Chats.Where(c => c.Type == ChatType.Personal && c.ChatMessages.Count > 0))
            {
                var LastMessage = chat.ChatMessages.OrderByDescending(m => m.Date).FirstOrDefault(m => m.IsDeleted == false);
                chatList.Add(new ChatBriefInfoDTO()
                {
                    ChatId = chat.ChatId,
                    Name = chat.ChatMembers.FirstOrDefault(u => u.UserId != UserId).Nickname,
                    DateLastMessage = LastMessage != null ? LastMessage.Date : DateTime.Now,
                    TextLastMessage = LastMessage != null ? LastMessage.Text : ""
                });
            }

            return chatList;
        }

        public async Task<IEnumerable<ChatBriefInfoDTO>> GetGroupChats(int UserId)
        {
            var user = await _db.Users.Include(u => u.Chats.Where(c => c.Type == ChatType.Group)).ThenInclude(c => c.ChatMessages.Where(m => m.IsDeleted != true).OrderByDescending(m => m.Date).Take(1)).FirstOrDefaultAsync(u => u.UserId == UserId);
            var chatList = new List<ChatBriefInfoDTO>(user.Chats.Count(c => c.Type == ChatType.Group));
            foreach (var chat in user.Chats.Where(c => c.Type == ChatType.Group))
            {
                var LastMessage = chat.ChatMessages.OrderByDescending(m => m.Date).FirstOrDefault(m => m.IsDeleted == false);
                chatList.Add(new ChatBriefInfoDTO()
                {
                    ChatId = chat.ChatId,
                    Name = chat.Name,
                    DateLastMessage = LastMessage != null ? LastMessage.Date : DateTime.Now,
                    TextLastMessage = LastMessage != null ? LastMessage.Text : ""
                });
            }

            return chatList;
        }

    }
}

    

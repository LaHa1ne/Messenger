using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messenger2.DataLayer.DTO;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool> IsUserExistsByNickname(string Nickname)
        {
            return await _db.Users.AnyAsync(u => u.Nickname.ToUpper() == Nickname.ToUpper());
        }
        public async Task<bool> IsUserExistsByEmail(string Email)
        {
            return await _db.Users.AnyAsync(u => u.Email.ToUpper() == Email.ToUpper());
        }

        public async Task<User> GetByNickname(string Nickname)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Nickname.ToUpper() == Nickname.ToUpper());
        }

        public async Task<User> GetByUserId(int UserId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
        }

        public async Task<User> GetByEmailAndPassword(string Email, string Password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == Email.ToUpper() && u.Password == Password);
        }

        public async Task<IEnumerable<UserBriefInfoDTO>> GetFriends(int UserId)
        {
            var user = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserId == UserId);
            return user.Friends.Select(u=> new UserBriefInfoDTO(u.UserId,u.Nickname)).ToList();
        }

        public async Task<ICollection<User>> GetSenders(int UserId)
        {
            var user = await GetByUserId(UserId);
            return user.Senders;
        }

        public async Task AcceptFriendRequest(int UserId, int SenderId)
        {
            User user1 = await GetByUserId(UserId);
            User user2 = await GetByUserId(SenderId);
            user1.Senders.Remove(user2);
            user2.Senders.Remove(user1);
            user1.Friends.Add(user2);
            user2.Friends.Add(user1);
            await _db.SaveChangesAsync();
        }
        public async Task RejectFriendRequest(int UserId, int SenderId)
        {
            User user1 = await GetByUserId(UserId);
            User user2 = await GetByUserId(SenderId);
            user1.Senders.Remove(user2);
            await _db.SaveChangesAsync();
        }

        public async Task SendFriendRequest(int SenderId, int UserId)
        {
            User user1 = await GetByUserId(SenderId);
            User user2 = await GetByUserId(UserId);
            user2.Senders.Add(user1);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteFriend(int UserId, int FriendId)
        {
            User user1 = await GetByUserId(UserId);
            User user2 = await GetByUserId(FriendId);
            user1.Friends.Remove(user2);
            user2.Friends.Remove(user1);
            await _db.SaveChangesAsync();
        }

    }
}

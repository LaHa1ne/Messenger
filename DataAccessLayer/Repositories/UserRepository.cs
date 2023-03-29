using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messenger2.DataLayer.DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>,IUserRepository
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

        public async Task<User> GetUserByEmailAndPassword(string Email, string HashPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == Email.ToUpper());
            return user != null ? user.Password == HashPassword ? user : null : null;
        }

        public async Task<IEnumerable<UserBriefInfoDTO>> GetFriends(int UserId)
        {
            var user = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserId == UserId);
            return user.Friends.Select(u=> new UserBriefInfoDTO(u.UserId,u.Nickname)).ToList();
        }

        public async Task<IEnumerable<UserBriefInfoDTO>> GetSenders(int UserId)
        {
            var user = await _db.Users.Include(u => u.Senders).FirstOrDefaultAsync(u => u.UserId == UserId);
            return user.Senders.Select(u => new UserBriefInfoDTO(u.UserId, u.Nickname)).ToList();
        }

        public async Task<bool> AcceptFriendRequest(int UserId, int SenderId)
        {
            User user = await _db.Users.Include(u => u.Friends).Include(u=>u.Senders).FirstOrDefaultAsync(u => u.UserId == UserId);
            User sender = await _db.Users.Include(u => u.Friends).Include(u => u.Senders).FirstOrDefaultAsync(u => u.UserId == SenderId);
            user.Senders.Remove(sender);
            sender.Senders.Remove(user);
            user.Friends.Add(sender);
            sender.Friends.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RejectFriendRequest(int UserId, int SenderId)
        {
            User user = await _db.Users.Include(u => u.Senders).FirstOrDefaultAsync(u => u.UserId == UserId);
            User sender = await _db.Users.FirstOrDefaultAsync(u => u.UserId == SenderId);
            user.Senders.Remove(sender);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SendFriendRequest(int SenderId, string UserNickname)
        {
            User user = await _db.Users.Include(u => u.Senders).FirstOrDefaultAsync(u => u.Nickname== UserNickname);
            if (user == null) { return false; }
            User sender = await _db.Users.FirstOrDefaultAsync(u => u.UserId == SenderId);
            user.Senders.Add(sender);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFriend(int UserId, int FriendId)
        {
            User user = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserId == UserId);
            User friend = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserId == FriendId);
            user.Friends.Remove(friend);
            friend.Friends.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}

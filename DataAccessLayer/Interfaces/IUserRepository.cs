using DataLayer.Entities;
using messenger2.DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsUserExistsByNickname(string Nickname);
        Task<bool> IsUserExistsByEmail(string Email);
        Task<User> GetByNickname(string Nickname);
        Task<User> GetByUserId(int UserId);
        Task<User> GetUserByEmailAndPassword(string Email, string HashPassword);
        Task<IEnumerable<UserBriefInfoDTO>> GetFriends(int UserId);
        Task<IEnumerable<UserBriefInfoDTO>> GetSenders(int UserId);
        Task<bool> AcceptFriendRequest(int UserId, int SenderId);
        Task<bool> RejectFriendRequest(int UserId, int SenderId);
        Task<bool> SendFriendRequest(int SenderId, string UserNickname);
        Task<bool> DeleteFriend(int UserId, int FriendId);

    }
}

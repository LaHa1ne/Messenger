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
        Task<User> GetByEmailAndPassword(string Email, string Password);
        Task<IEnumerable<UserBriefInfoDTO>> GetFriends(int UserId);
        Task<IEnumerable<UserBriefInfoDTO>> GetSenders(int UserId);
        Task<int> AcceptFriendRequest(int UserId, int SenderId);
        Task<int> RejectFriendRequest(int UserId, int SenderId);
        Task<bool> SendFriendRequest(int SenderId, string UserNickname);
        Task<int> DeleteFriend(int UserId, int FriendId);

    }
}

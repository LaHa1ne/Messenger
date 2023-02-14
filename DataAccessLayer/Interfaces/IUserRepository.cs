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
        Task<ICollection<User>> GetSenders(int UserId);
        Task AcceptFriendRequest(int UserId, int SenderId);
        Task RejectFriendRequest(int UserId, int SenderId);
        Task SendFriendRequest(int SenderId, int UserId);
        Task DeleteFriend(int UserId, int FriendId);

    }
}

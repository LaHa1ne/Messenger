using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using messenger2.DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataAccessLayer.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<Message> GetByMessageId(int MessageId);
    }
}

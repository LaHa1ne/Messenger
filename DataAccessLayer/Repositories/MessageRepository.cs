using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataLayer.Entities;
using messenger2.DataAccessLayer.Interfaces;
using messenger2.DataLayer.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataAccessLayer.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Message> GetByMessageId(int MessageId)
        {
            return await _db.Messages.FirstOrDefaultAsync(m => m.MessageId == MessageId);
        }

    }
}

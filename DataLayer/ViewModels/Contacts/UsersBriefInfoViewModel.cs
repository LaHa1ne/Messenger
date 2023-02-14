using messenger2.DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.ViewModels.Contacts
{
    public class UsersBriefInfoViewModel
    {
        public IEnumerable<UserBriefInfoDTO> UsersInfo { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }
}

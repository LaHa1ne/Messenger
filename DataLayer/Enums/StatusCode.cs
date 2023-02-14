using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.Enums
{
    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        UsersNotExists,
        UserExists

    }
}

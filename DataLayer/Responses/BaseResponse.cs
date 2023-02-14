using messenger2.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.Responses
{
    public class BaseRepsonse<Tobject> : IBaseResponse<Tobject>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public Tobject Data { get; set; }
    }

    public interface IBaseResponse<Tobject>
    {
        Tobject Data { get; set; }
    }
}

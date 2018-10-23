using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestDTO
{
    public class OrderDTO
    {
        public long OrderID { get; set; }
        public int OrAgentCompanyID { get; set; }
        public int OrAgentId { get; set; }
        public string OrGivenID { get; set; }

    }
}

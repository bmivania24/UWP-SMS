using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UW_Solution
{
    public class MsgResponseTwModel
    {
        public int IdMessageResponseTw { get; set; }
        public int IdMessage { get; set; }
        public string DateSent { get; set; }
        public string confirmationCode { get; set; }
        public string dateCreated { get; set; }
        public string ToNumber { get; set; }
        public string FromNumber { get; set; }
        public string Message { get; set; }
    }
}

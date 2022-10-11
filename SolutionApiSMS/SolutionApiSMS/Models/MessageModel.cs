using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSMS.Models
{
    public class MessageModel
    {
        public int idMessage { get; set; }
        public string dateCreated { get; set; }
        public string ToNumber { get; set; }
        public string FromNumber { get; set; }
        public string Message { get; set; }
      
        public List<MessageModel> items { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageObject;

namespace DataLayer
{
    public class MessageList
    {
        private List<Message> _messageList = new List<Messages>();
        public List<Message> messageList { get { return _messageList; } }
    }
}

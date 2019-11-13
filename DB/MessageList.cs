using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;

namespace DB
{
    public class MessageList
    {
        private List<Message> _messageList = new List<Message>();
        public List<Message> messageList { get { return _messageList; } }

        public void add(Message newMessage)
        {
            messageList.Add(newMessage);
        }


    }
}

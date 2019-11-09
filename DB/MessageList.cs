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


        private string[] file = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\Messages.csv");
        public string line = "";
        bool isAlrThere = false;

        public string createFile()
        {
            if (isAlrThere == true)
            {
                foreach (Message m in messageList)
                {
                    line = m.ID + m.MessageType + m.Sender + m.Subject + m.MessageText + Environment.NewLine;
                }
            }
            return line;
        }

        public void add(Message newMessage)
        {
            messageList.Add(newMessage);
            createFile();
            File.AppendAllText(@"..\..\..\..\SoftEngCoursework\Messages.csv", createFile());
        }
    }
}

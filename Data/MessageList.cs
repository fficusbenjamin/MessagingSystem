using MessageObject;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class MessageList
    {
        private List<Message> _messageList = new List<Message>();
        public List<Message> messageList { get { return _messageList; } }


        private string[] file = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\Messages.json");
        public string line = "";
        bool isAlrThere = false;

        public string createFile()
        {
            if (isAlrThere == true)
            {
                foreach (Message m in messageList)
                {
                    line = m.ID + m.MessageType + m.Sender + m.Subject + m.MessageText;
                }
            }
            return line;
        }

        public void add(Message newMessage)
        {
            messageList.Add(newMessage);
            createFile();
            File.AppendAllText(@"..\..\..\..\SoftEngCoursework\Messages.json", createFile());
        }
    }
}

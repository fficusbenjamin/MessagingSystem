using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MessageObject
{
    class Sms : Message
    {
        private readonly string _messageType;
        private string _messageID,_sender,_messageText,_subject;

        public Sms(string messageID, string sender, string messageText,string subject) 
        {
            _messageType = "SMS";
            _messageID = messageID;
            _sender = sender;
            _messageText = messageText;
            _subject = null;
        }

        public override string MessageType 
        {
            get { return _messageType; }
        }
        public override string ID 
        {
            get { return _messageID; }
            set { _messageID = value; }
        }
        public override string Sender 
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public override string MessageText 
        {
            get { return _messageText; }
            set { _messageText = value; }
        }
        public override string Subject 
        {
            get { return _subject; }
            set { _subject = value; }
        }
    }
}

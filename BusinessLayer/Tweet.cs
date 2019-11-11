using System;

namespace BusinessLayer
{
    class Tweet : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _messageText, _subject,_body;

        public Tweet(string messageID, string sender, string messageText, string subject, string body)
        {
            _messageType = "Tweet";
            _messageID = messageID;
            _sender = sender;
            _messageText = messageText;
            _subject = subject;
            _body = body;
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
            set 
            {
                valSender(value);
                _sender = value; 
            }
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
        public override string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public string valSender(string val)
        {
            
            System.Text.RegularExpressions.Regex rTwtHnd = new System.Text.RegularExpressions.Regex(@"[@][a-zA-z0-9]{0,15}");
            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, Twitter handle");

            }
            if (!rTwtHnd.IsMatch(val))
            {
                val = null;
                throw new Exception("Field is not a valid Twitter handle");

            }
            return val;
        }
    }
}

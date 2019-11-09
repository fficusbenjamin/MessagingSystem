namespace BusinessLayer
{
    class Sms : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _messageText, _subject,_body;

        public Sms(string messageID, string sender, string messageText, string subject, string body)
        {
            _messageType = "SMS";
            _messageID = messageID;
            _sender = sender;
            _messageText = messageText;
            _subject = null;
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
            set { _sender = value; }

            //in the set validate it
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
    }
}

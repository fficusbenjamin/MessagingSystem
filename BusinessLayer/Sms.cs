using System;

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
            System.Text.RegularExpressions.Regex rNmbr = new System.Text.RegularExpressions.Regex(@"(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s\.]?[(]?[0-9]{1,3}[)]?([-\s\.]?[0-9]{3})([-\s\.]?[0-9]{3,4})");
            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, number");

            }
            if (!rNmbr.IsMatch(val))
            {
                val = null;
                throw new Exception("Field is not a valid mobile number");

            }
            return val;
        }
    }
}

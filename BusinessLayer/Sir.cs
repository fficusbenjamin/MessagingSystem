using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   class Sir : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _messageText, _subject, _body;
        public Sir(string messageID, string sender, string subject, string body)
        {
            _messageType = "Email";
            _messageID = messageID;
            _sender = sender;
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
        public override string MessageText 
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        public override string Sender
        {
            get { return _sender; }
            set {
                valSender(value);
                _sender = value; }
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

        
        public string valSubj(string val)
        {

            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, subject");

            }

            if (val.Length > 20)
            {
                val = null;
                throw new Exception("Subject is too long");

            }

            return val;

        }
        public string valSender(string val)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, email address");

            }
            if (!rEmail.IsMatch(val))
            {
                val = null;
                throw new Exception("Field is not a valid email address");

            }
            return val;
        }

    }
}

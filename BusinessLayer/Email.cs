using System;

namespace BusinessLayer
{
    class Email : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _subject,_body;
        


        public Email(string messageID, string sender, string subject, string body)
        {
            _messageType = "Email";
            _messageID = messageID;
            _sender = sender;
            _subject = subject;
            _body = body;
        }

        public Email() 
        {
            _messageType = "Email";
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

        public override string Subject
        {
            get { return _subject; }
            set 
            {
                valSub(value);
                _subject = value;

            }     
        }
        public override string Body
        {
            get { return _body; }
            set {
                valBd(value);
                _body = value; }
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

        public string valSub(string val) 
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

        

        

        public string valBd(string val)
        {
            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, body of the email");

            }
            if (val.Length > 1028)
            {
                val = null;
                throw new Exception("Email cannot be longer than 1028 characters");

            }
            return val;
        }

    }
}

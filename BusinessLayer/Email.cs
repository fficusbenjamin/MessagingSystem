using System;

namespace BusinessLayer
{
    class Email : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _messageText, _subject,_body;
        


        public Email(string messageID, string sender, string messageText, string subject, string body)
        {
            _messageType = "Email";
            _messageID = messageID;
            _messageText = messageText;
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
            set {_messageText = value; }
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
                valSubj(value);
                _subject = value;

            }     
        }
        public override string Body
        {
            get { return _body; }
            set { _body = value; }
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

        public string valSubj(string val)
        {
            System.Text.RegularExpressions.Regex lnght = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][0-9][,./-]{20}");
            //try
            //{
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

            //}
            //catch (Exception execMsg)
            //{
            //    MessageBox.Show(execMsg.Message);
            //}
            return val;

        }

        public string valBody(string val)
        {
            System.Text.RegularExpressions.Regex lnght = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][0-9][,./-]{20}");
            //try
            //{
                if (val == "")
                {
                    val = null;
                    throw new Exception("Field cannot be blank, body");

                }

                if (!lnght.IsMatch(val))
                {
                    val = null;
                    throw new Exception("Subject is too long");

                }

            //}
            //catch (Exception execMsg)
            //{
            //    MessageBox.Show(execMsg.Message);
            //}
            return val;

        }

    }
}

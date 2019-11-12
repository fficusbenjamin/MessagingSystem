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

        private string _messageID, _sender, _subject, _body;
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
            set {
                valBd(value);
                _body = value; }
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

        private string getFrstLine()
        {
            string[] line = _body.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
            return line[0];
        }
        private string getScdLine()
        {
            string[] line = _body.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
            return line[1];
        }

        public string valBd(string val) 
        {
            System.Text.RegularExpressions.Regex rCode = new System.Text.RegularExpressions.Regex(@"[0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]");
            List<string> NatInc = new List<string>();
            NatInc.Add("Theft of Properties");
            NatInc.Add("Staff Attack");
            NatInc.Add("Device Damage");
            NatInc.Add("Raid");
            NatInc.Add("Customer Attack");
            NatInc.Add("Staff Abuse");
            NatInc.Add("Bomb Threat");
            NatInc.Add("Terrorism");
            NatInc.Add("Sport Injury");
            NatInc.Add("Personal Info Leak");

            foreach (string inc in NatInc )
            {
                if (getScdLine() != inc) 
                {
                    val = null;
                    throw new Exception("Field is not a valid Incident");
                }
            }


            if (!rCode.IsMatch(getFrstLine())) 
            {
                val = null;
                throw new Exception("Field is not a valid Sport Centre Code");
            }
            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, Sport Centre Code");

            }


            return val;
        }

        
        

    }
}

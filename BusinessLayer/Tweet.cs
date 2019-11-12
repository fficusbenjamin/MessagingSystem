using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessLayer
{
    class Tweet : Message
    {
        private readonly string _messageType;
        private string _messageID, _sender, _subject,_body;
        public List<string> tagList = new List<string>();
        public List<string> mntsList = new List<string>();

        public Tweet(string messageID, string sender, string subject, string body)
        {
            _messageType = "Tweet";
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
            set 
            {
                valSender(value);
                _sender = value; 
            }
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

        public string valBd(string val) 
        {
            System.Text.RegularExpressions.Regex rHsh = new System.Text.RegularExpressions.Regex(@"\B(\#[a-zA-Z]+\b)(?!;)");

            Dictionary<string, string> textspeak = new Dictionary<string, string>();

            foreach (string line in File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\textwords.csv"))
            {
                string[] keyvalue = line.Split(',');
                if (keyvalue.Length == 2)
                {
                    textspeak.Add(keyvalue[0], keyvalue[1]);
                }


            }
            string[] msgSplit = val.Split(' ');

            foreach (var entry in textspeak)
            {
                val = val.Replace(entry.Key, entry.Key + " <" + entry.Value + ">");
            }

            for (int i = 0; i < msgSplit.Length; i++)
            {
                if (rHsh.IsMatch(msgSplit[i]))
                {
                    tagList.Add(msgSplit[i]);
                }
            }

            //Get mentions
            for (int i = 0; i < val.Length; i++)
            {
                if (msgSplit[i][0] == '@')
                {
                    mntsList.Add(msgSplit[i]);
                }
            }
            System.IO.File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\hashtags.txt", tagList);
            System.IO.File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\mentions.txt", mntsList);

            return val;
        }
    }
}

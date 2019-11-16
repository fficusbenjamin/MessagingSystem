using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
        
        public override string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        public override string Body
        {
            get { return _body; }
            set 
            {
                //valBd(value);
                _body = valBd(value); 
            }
        }
        
        
        
        
        
        Regex rTwtHnd = new Regex(@"[@][a-zA-z0-9]{0,15}");

        public string valSender(string val)
        {
            
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

            if (val == "")
            {
                val = null;
                throw new Exception("Field cannot be blank, Twitter handle");

            }

            if (val.Length > 140)
            {
                val = null;
                throw new Exception("Text cannot be longer than 140 characters");

            }
            Regex rHsh = new Regex(@"\B(\#[a-zA-Z]+\b)(?!;)");

            Dictionary<string, string> textspeak = new Dictionary<string, string>();

            foreach (string line in File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\textwords.csv"))
            {
                string[] keyvalue = line.Split(',');
                if (keyvalue.Length == 2)
                {
                    textspeak.Add(keyvalue[0], keyvalue[1]);
                }


            }
            
            Regex rSplit = new Regex(@"[\d\s]");
            

            string[] msgSplit = rSplit.Split(val);
            
            
            

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
            for (int i = 0; i < msgSplit.Length; i++)
            {
                if (rTwtHnd.IsMatch(msgSplit[i]))
                {
                    mntsList.Add(msgSplit[i]);
                }
            }
            File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\hashtags.txt", tagList);
            File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\mentions.txt", mntsList);

            

            return val;
        }
    }
}

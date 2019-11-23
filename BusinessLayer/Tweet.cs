using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


///-------------------------------------------------------------------
///   Class:          Tweet
///   Description:    Class for the Tweet message that inherit
///                   and extend the abstract message class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------

namespace BusinessLayer
{
    class Tweet : Message
    {
        //declare all the variable for the properties
        private readonly string _messageType;
        private string _messageID, _sender, _subject,_body;
        //declare the lists for the hashtags and the mentions
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
        //getters and setters for the class properties
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
            {   //validate and set the value
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
            {   //validate and set the value                
                _body = valBd(value); 
            }
        }   
        //regular expression for the twitter handle
        Regex rTwtHnd = new Regex(@"[@][a-zA-z0-9]{0,15}");
        //method that validate the sender
        public string valSender(string val)
        {
            //if the sender is blank
            if (val == "")
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field cannot be blank, Twitter handle");

            }
            //if the sender is not a twitter handle
            if (!rTwtHnd.IsMatch(val))
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field is not a valid Twitter handle");
            }
            return val;
        }
        //method that validates the body
        public string valBd(string val) 
        {
            //if the body is blank
            if (val == "")
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field cannot be blank, Twitter handle");
            }
            //if the body is longer than 140 characters
            if (val.Length > 140)
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Text cannot be longer than 140 characters");
            }
            //regular expression for the hashtags
            Regex rHsh = new Regex(@"\B(\#[a-zA-Z]+\b)(?!;)");
            //creates a new dictionary from the csv file provided by the lecturer
            Dictionary<string, string> textspeak = new Dictionary<string, string>();
            //for each line in the file
            foreach (string line in File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\textwords.csv"))
            {
                //creates an array of strings splitted by commas
                string[] keyvalue = line.Split(',');
                //if the value lenght is equal 2
                if (keyvalue.Length == 2)
                {
                    //adds it to the dictionary first and second value
                    textspeak.Add(keyvalue[0], keyvalue[1]);
                }
            }
            //regular expressio  for the space split
            Regex rSplit = new Regex(@"[\d\s]");
            //split the message by the space
            string[] msgSplit = rSplit.Split(val);
            //for each value found in the dictionary         
            foreach (var entry in textspeak)
            {
                //replace it with the same plus the "explanation"
                val = val.Replace(entry.Key, entry.Key + " <" + entry.Value + ">");
            }
            //loop that checks the hashtags and add them to the list
            for (int i = 0; i < msgSplit.Length; i++)
            {
                if (rHsh.IsMatch(msgSplit[i]))
                {
                    tagList.Add(msgSplit[i]);
                }
            }

            //loop that checks the mentions and add them to the list
            for (int i = 0; i < msgSplit.Length; i++)
            {
                if (rTwtHnd.IsMatch(msgSplit[i]))
                {
                    mntsList.Add(msgSplit[i]);
                }
            }
            //append the list to the relative files
            File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\hashtags.txt", tagList);
            File.AppendAllLines(@"..\..\..\..\SoftEngCoursework\mentions.txt", mntsList);          
            return val;
        }
    }
}

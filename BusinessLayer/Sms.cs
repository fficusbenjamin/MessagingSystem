using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

///-------------------------------------------------------------------
///   Class:          Sms
///   Description:    Class for the Sms message that inherit
///                   and extend the abstract message class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------

namespace BusinessLayer
{
    class Sms : Message
    {
        //declare all the variable for the properties
        private readonly string _messageType;
        private string _messageID, _sender, _subject,_body;

        public Sms(string messageID, string sender, string subject, string body)
        {
            _messageType = "SMS";
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


        public string valSender(string val)
        {
            //regular expression for the mobile number
            System.Text.RegularExpressions.Regex rNmbr = new System.Text.RegularExpressions.Regex(@"(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s\.]?[(]?[0-9]{1,3}[)]?([-\s\.]?[0-9]{3})([-\s\.]?[0-9]{3,4})");
            //if the number is blank
            if (val == "")
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field cannot be blank, number");
            }
            //if the number is not valid
            if (!rNmbr.IsMatch(val))
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field is not a valid mobile number");
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
                throw new Exception("Field cannot be blank, text");
            }
            //if the body is longer than 140 characters
            if (val.Length > 140)
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Text cannot be longer than 140 characters");
            }
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
            //split the message by the spaces
            string[] msgSplit = rSplit.Split(val);
            //for each value found in the dictionary 
            foreach (var entry in textspeak)
            {
                //replace it with the same plus the "explanation"
                val = val.Replace(entry.Key, entry.Key + " <" + entry.Value + ">");
            }            
            return val;
        }
    }
}

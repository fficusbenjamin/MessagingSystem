using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

///-------------------------------------------------------------------
///   Class:          Email
///   Description:    Class for the Email message that inherit
///                   and extend the abstract message class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------



namespace BusinessLayer
{
    class Email : Message
    {
        //declare all the variable for the properties
        private readonly string _messageType;
        private string _messageID, _sender, _subject,_body;
        //creates the list for the URLs quarantined
        public List<string> qrntList = new List<string>();



        public Email(string messageID, string sender, string subject, string body)
        {
            _messageType = "Email";
            _messageID = messageID;
            _sender = sender;
            _subject = subject;
            _body = body;
        }
        //getters and setters for the class properties
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
            {   //validate and set the value
                valSender(value);
                _sender = value;             
            }
        }
        public override string Subject
        {
            get { return _subject; }
            set 
            {   //validate and set the value
                valSub(value);
                _subject = value;
            }     
        }
        public override string Body
        {
            get { return _body; }
            set 
            {   //validate and set the value
                _body = valBd(value); 
            }
        }
        //method that validate the sender
        public string valSender(string val) 
        {
                //regular expression for the email address
                Regex rEmail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                //if the sender is blank
                if (val == "")
                {
                    //set the value to null nad throws the exception
                    val = null;
                    throw new Exception("Field cannot be blank, email address");
                    
                }
                //if the email address is not valid
                if (!rEmail.IsMatch(val))
                {
                    //set the value to null nad throws the exception
                    val = null;
                    throw new Exception("Field is not a valid email address");                    
                }                            
            return val;
        }
        //method that validates the subject
        public string valSub(string val) 
        {
            //if the subject is blank
            if (val == "")
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field cannot be blank, subject");
            }
            //if the subject is bigger than 20 characters
            if (val.Length > 20)
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Subject is too long");
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
                throw new Exception("Field cannot be blank, body of the email");

            }
            //if the body is longer than 1028 characters
            if (val.Length > 1028)
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Email cannot be longer than 1028 characters");
            }
            //regular expression for the URLs in the body
            Regex rUrl = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$");
            //regular expression for the spaces
            Regex rSplit = new Regex(@"[\d\s]");
            //array of string that stores the values splitted by the spaces
            string[] msgSplit = rSplit.Split(val);
            //for every entry in the message splitted
            foreach (string entry in msgSplit)
            {
                //if there is an URL
                if (rUrl.IsMatch(entry)) 
                {
                    //add it to the qurantine list and replace it with the message
                    qrntList.Add(entry);
                    val = val.Replace(entry,"<URL Quarantined>");
                }                
            }
            return val;
        }

    }
}

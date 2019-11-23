using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


///-------------------------------------------------------------------
///   Class:          Sir
///   Description:    Class for the Sir message that inherit
///                   and extend the abstract message class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------



namespace BusinessLayer
{
   class Sir : Message
    {
        //declare all the variable for the properties
        private readonly string _messageType;
        private string _messageID, _sender, _subject, _body;
        //creates a list for the sir
        public List<string> sirList = new List<string>();


        
        public Sir(string messageID, string sender, string subject, string body)
        {
            _messageType = "Email";
            _messageID = messageID;
            _sender = sender;
            _subject = subject;
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
                _body = value;
                _body = valBd(value);
            }
        }

        //method that validates the subject
        public string valSubj(string val)
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
        //method that take the first line from the body
        private string getFrstLine()
        {
            string[] line = _body.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
            return line[0];
        }
        //method that takes the second line from the body
        private string getScdLine()
        {
            string[] line = _body.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
            return line[1];
        }
        //method that validates the body
        public string valBd(string val) 
        {
            //regular expression fon the incident code
            Regex rCode = new Regex(@"[0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]");
            //list of incidents
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
            //stores the incident code
            string line = getScdLine();
            bool test = false;
            //checks the incidents in the list
            foreach (string inc in NatInc )
            {
                if (line == inc) 
                {
                    test = false;
                }
            }
            //if the bool is true the incident is not in the list and throws the exception
            if (test == true)
            {
                throw new Exception("Not a valid incident.");
            }

            // the code is not mached
            if (!rCode.IsMatch(getFrstLine())) 
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field is not a valid Sport Centre Code");
            }
            //if the body is blank
            if (val == "")
            {
                //set the value to null nad throws the exception
                val = null;
                throw new Exception("Field cannot be blank, Sport Centre Code");
            }
            //creates a sir string
            string sirItem = getFrstLine() + ", " + getScdLine()+"\r\n";
            //add it to the file
            File.AppendAllText(@"..\..\..\..\SoftEngCoursework\sirlist.txt", sirItem);
            return val;
        }      
    }
}

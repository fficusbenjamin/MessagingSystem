using System;

///-------------------------------------------------------------------
///   Class:          Message
///   Description:    Abstract Class for the message that set the 
///                   properties and has a method to take the type     
///                   and creates the appropriate type of message
/// 
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------

namespace BusinessLayer
{
    //declare the properties
    public abstract class Message
    {
        public abstract string MessageType { get; }
        public abstract string ID { get; set; }
        public abstract string Sender { get; set; }
        
        public abstract string Subject { get; set; }
        public abstract string Body { get; set; }

        

        //method to take id and creates the messages
        public string getType()
        {
            switch (ID)
            {
                case "S":
                    ID = "SMS";
                    break;

                case "T":
                    ID = "Tweet";
                    break;

                case "E":
                    ID = "Email";
                    break;
            }
            return ID;
        }
       
        

    }
}

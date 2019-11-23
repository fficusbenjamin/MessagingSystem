using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;


///-------------------------------------------------------------------
///   Class:          MessageList
///   Description:    This class hold all the properties and for the 
///                   list of messages in the application
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------



namespace DB
{
    public class MessageList
    {
        //creates the list
        private List<Message> _messageList = new List<Message>();
        //get for the lsit
        public List<Message> messageList { get { return _messageList; } }

        //method to add to the list
        public void add(Message newMessage)
        {
            messageList.Add(newMessage);
        }


    }
}

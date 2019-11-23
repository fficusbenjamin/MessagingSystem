///-------------------------------------------------------------------
///   Class:          TweetFactory
///   Description:    Factory class for the Tweet message that inherit
///                   and extend the message factory class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------




namespace BusinessLayer
{
    public class TweetFactory : MessageFactory
    {
        //creates the class properties
        private string _messageID, _sender, _subject, _body;
        //the factory takes the ID
        public TweetFactory(string ID)
        {
            _messageID = ID;
        }
        //method that returns a new object for the class
        public override Message GetMessageType()
        {
            return new Tweet(_messageID, _sender, _subject, _body);
        }
        
    }
}

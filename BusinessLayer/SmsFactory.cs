///-------------------------------------------------------------------
///   Class:          SmsFactory
///   Description:    Factory class for the Sms message that inherit
///                   and extend the message factory class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------





namespace BusinessLayer
{
    public class SmsFactory : MessageFactory
    {
        //creates the class properties
        private string _messageID, _sender, _subject, _body;
        //creates the class properties
        public SmsFactory(string ID)
        {
            _messageID = ID;
        }
        //method that returns a new object for the class
        public override Message GetMessageType()
        {
            return new Sms(_messageID, _sender,_subject, _body);
        }       
    }
}

///-------------------------------------------------------------------
///   Class:          SirFactory
///   Description:    Factory class for the SIR message that inherit
///                   and extend the message factory class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------




namespace BusinessLayer
{
    public class SirFactory : MessageFactory
    {
        //creates the class properties
        private string _messageID, _sender, _subject, _body;
        //the factory takes the ID
        public SirFactory(string ID)
        {
            _messageID = ID;
        }
        //method that returns a new object for the class
        public override Message GetMessageType()
        {
            return new Sir(_messageID, _sender, _subject, _body);
        }
        
    }
}


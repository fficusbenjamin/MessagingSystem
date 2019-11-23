///-------------------------------------------------------------------
///   Class:          MessageWindow (Window)
///   Description:    Abstract factory class for the abstract message
///                   class.    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------



namespace BusinessLayer
{
    public abstract class MessageFactory
    {
        public abstract Message GetMessageType();
        
    }
}

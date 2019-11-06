using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageObject
{
    public class SmsFactory : MessageFactory
    {
        private string _messageID, _sender, _messageText, _subject;

        public SmsFactory(string ID) 
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Sms(_messageID,_sender,_messageText,_subject);
        }
    }
}

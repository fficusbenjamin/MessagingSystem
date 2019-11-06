using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageObject
{
    public class EmailFactory : MessageFactory
    {
        private string _messageID, _sender, _messageText, _subject;

        public EmailFactory(string ID)
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Email(_messageID, _sender, _messageText, _subject);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageObject
{
    public class TweetFactory : MessageFactory
    {
        private string _messageID, _sender, _messageText, _subject;

        public TweetFactory(string ID)
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Tweet(_messageID, _sender, _messageText, _subject);
        }
    }
}

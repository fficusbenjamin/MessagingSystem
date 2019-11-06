using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MessageObject
{
    public abstract class MessageFactory
    {
        public abstract Message GetMessageType();
    }
}

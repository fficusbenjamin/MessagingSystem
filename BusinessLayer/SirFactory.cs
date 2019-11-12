namespace BusinessLayer
{
    public class SirFactory : MessageFactory
    {
        private string _messageID, _sender, _subject, _body;

        public SirFactory(string ID)
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Sir(_messageID, _sender, _subject, _body);
        }
        
    }
}


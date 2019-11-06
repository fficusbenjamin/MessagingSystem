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

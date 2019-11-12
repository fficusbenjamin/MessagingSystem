namespace BusinessLayer
{
    public class EmailFactory : MessageFactory
    {
        private string _messageID, _sender, _subject,_body;

        public EmailFactory(string ID)
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Email(_messageID, _sender, _subject,_body);
        }
       
    }
}

namespace BusinessLayer
{
    public class SmsFactory : MessageFactory
    {
        private string _messageID, _sender, _messageText, _subject, _body;

        public SmsFactory(string ID)
        {
            _messageID = ID;
        }

        public override Message GetMessageType()
        {
            return new Sms(_messageID, _sender, _messageText, _subject, _body);
        }
       
    }
}

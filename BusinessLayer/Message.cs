using System;

namespace BusinessLayer
{
    public abstract class Message
    {
        public abstract string MessageType { get; }
        public abstract string ID { get; set; }
        public abstract string Sender { get; set; }
        public abstract string MessageText { get; set; }
        public abstract string Subject { get; set; }
        public abstract string Body { get; set; }

        


        public string getType()
        {
            switch (ID)
            {
                case "S":
                    ID = "SMS";
                    break;

                case "T":
                    ID = "Tweet";
                    break;

                case "E":
                    ID = "Email";
                    break;
            }
            return ID;
        }
        public string getSubject() 
        {
            System.Text.RegularExpressions.Regex rSir = new System.Text.RegularExpressions.Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");

            if (rSir.IsMatch(Subject)) 
            {
                Sir sir = new Sir(ID,Sender,Subject,Body);
            }
            return Subject;
        }
        

    }
}

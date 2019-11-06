﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MessageObject
{
    public abstract class Message
    {
        public abstract string MessageType { get; }
        public abstract string ID { get; set; }
        public abstract string Sender { get; set; }
        public abstract string MessageText { get; set; }
        public abstract string Subject { get; set; }


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
    }
}

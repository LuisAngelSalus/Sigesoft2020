using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class SuscriptionRegisterDto
    {
        public int SystemUserId { get; set; }
        public string Body { get; set; }
    }

    public class keySubscription
    {
        public string endpoint { get; set; }
        public ValueKey Keys { get; set; }
    }

    public class ValueKey
    {
        public string p256dh { get; set; }
        public string auth { get; set; }
    }

    public class SuscriptionPushDto
    {
        public string title { get; set; }
        public string message { get; set; }

    }
}

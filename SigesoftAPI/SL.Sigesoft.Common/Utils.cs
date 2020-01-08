using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Common
{
   public static class Utils
    {
        public static string Code(string prefix, string codeUser, int correlative)
        {
            return string.Format("{0}-{1}{2}", prefix, codeUser, correlative.ToString("000000000"));
        }
    }
}

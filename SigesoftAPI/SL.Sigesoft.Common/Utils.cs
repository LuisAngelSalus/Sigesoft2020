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

        public static string GetNewIdWin(int pintNodeId, int pintSequential, string pstrPrefix)
        {
            return string.Format("N{0}-{1}{2}", pintNodeId.ToString("000"), pstrPrefix, pintSequential.ToString("000000000"));
        }
    }
}

using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace Coursework.Models.Classes.Enternet
{
    static public class Connection
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
    }
}

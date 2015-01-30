using System;
using System.Security;

namespace Lugia.Helpers.Data
{
    static class SecureStringHelper
    {
        public static string Decrypt(this SecureString password)
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
            return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
        }
    }
}

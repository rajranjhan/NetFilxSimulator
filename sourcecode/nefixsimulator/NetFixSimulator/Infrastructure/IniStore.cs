using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AR.NETFixSimulator.Infrastructure
{
    public class IniStore : IConfigurationStore
    {
        [DllImport("Kernel32.dll", EntryPoint = "GetPrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPrivateProfileString(string lpAppName,
                                                         string lpKeyName,
                                                         string lpDefault,
                                                         [In, Out] char[] lpReturnString,
                                                         int nSize,
                                                         string lpFilename);
    }
}

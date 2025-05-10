using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public static class AppState
    {
        public static TcpConnectionManager Connection = new TcpConnectionManager();
        public static string CurrentUserId = "";
    }
}

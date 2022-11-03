using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MailClient.Network;

namespace MailClient.Network.Methods
{
    public class Check
    {
        public bool Checks(string mail, string password,string simvol)
        {
            string message = simvol+ "`" + mail + "`" + password;
            TCPClient tcp = new TCPClient();
            message=tcp.Tcpclient(message);
            if (message == "+")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

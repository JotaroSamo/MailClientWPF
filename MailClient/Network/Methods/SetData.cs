using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Network.Methods
{
    public class SetData
    {
        public void SetsDatas(string mess)
        {
            TCPClient tCPClient = new TCPClient();
            tCPClient.Tcpclient(mess);
        }
    }
}

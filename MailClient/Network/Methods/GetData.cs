using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Network.Methods
{
    public class GetData
    {
        public string GetDatas(string Mail)
        {
            TCPClient tCP = new TCPClient();
            return tCP.Tcpclient("Get data" + "`"+Mail);
        }
    }
}

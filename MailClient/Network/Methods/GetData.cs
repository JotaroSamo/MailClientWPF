using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Network.Methods
{
    public class GetData
    {
        public async Task<string> GetDatasH(string Mail)
        {
            TCPClient tCP = new TCPClient();
            return tCP.Tcpclient("Get Data" + "`"+Mail);
        }
        public string GetDatasW(string Mail)
        {
            TCPClient tCP = new TCPClient();
            return tCP.Tcpclient("Get DataW" + "`" + Mail);
        }
    }
}

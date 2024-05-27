using MailClient.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MailClient.Network.Methods
{
    public class SetData
    {
        public async Task SetsDatas(string mess)
        {
            TCPClient tCPClient = new TCPClient();
           await tCPClient.Tcpclient(mess);
        }
        public async Task UpdateUser(User user)
        {
            TCPClient tCPClient = new TCPClient();
            string userJsn = JsonSerializer.Serialize(user);
           await tCPClient.Tcpclient("/UpdateUser" + "`" + userJsn);
        }
    }
}

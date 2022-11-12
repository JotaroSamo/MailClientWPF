using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailClient.Network
{
    public class TCPClient
    {
       
        const int port = 8888;
        public TCPClient()
        {

        }
        public string Tcpclient(string mess)
        {
            string Host = Dns.GetHostName();
            string address = Dns.GetHostEntry(Host).AddressList[2].ToString();
            TcpClient? client = null;
            try
            {
               
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                byte[] data;
                 while (true)
                {

                    data = Encoding.Unicode.GetBytes(mess);

                    stream.Write(data, 0, data.Length);
                    data = new byte[512]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    client?.Close();
                }
                catch (Exception)
                {

                    
                }
               
            }
        }

    }
}

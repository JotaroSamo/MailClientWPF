using MailClient.Models.DBModels;
using MailClient.Network;
using MailClient.Network.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailClient
{
    /// <summary>
    /// Логика взаимодействия для MailContent.xaml
    /// </summary>
    public partial class MailContent : Window
    {
        string Mail;
        //private BindingList<MessegeMail> messegeMail;
        public MailContent(string Mail)
        {

            InitializeComponent();
            this.Mail = Mail;
          

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
           UpdateData();
           
        }


        private void SendMessage(object sender, RoutedEventArgs e)
        {
            MessegeMail mail = new MessegeMail() { DateTime = DateTime.Now, Topic = Tbox.Text, MailMess = Mbox.Text, IdHow = Mail, IdWhom = Hbox.Text + MailAdress.Text };
            string m = JsonSerializer.Serialize(mail);
            TCPClient tCPClient = new TCPClient();
            tCPClient.Tcpclient("Save data" + "`" + m);
            UpdateData();
        }
        public void UpdateData()
        {
            try
            {
                GetData getData = new GetData();
                var mail = JsonSerializer.Deserialize<MessegeMail[]>(getData.GetDatasH(Mail));
                OutMess.ItemsSource = mail;

              
                
                
            }
            catch (Exception ex)
            {


            }
        }

        private void OutMess_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessegeMail? messegeMail = OutMess.SelectedItem as MessegeMail;
                Outblock.Text = messegeMail?.MailMess;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void Out(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessegeMail? messegeMail = OutMess.SelectedItem as MessegeMail;
            TCPClient tCPClient = new TCPClient();
            tCPClient.Tcpclient("Delete" + "`" + messegeMail?.Id);
            UpdateData();
        }
    }
}


using MailClient.DB.Content;
using MailClient.Models.DBModels;
using MailClient.Network;
using MailClient.Network.Methods;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
           
           Task.Run( ()=> UpdateData());
           
        }
        private string filePath; // Переменная для хранения пути к выбранному файлу
        private void SelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                fileName.Content = openFileDialog.FileName;
            }
        }
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            List<FileMessage> fileMessage = new List<FileMessage>();
            if (!string.IsNullOrEmpty(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                // Отправка файла
                FileMessage fileMessages = new FileMessage()
                {
                    FileName = System.IO.Path.GetFileName(filePath),
                    FileType = System.IO.Path.GetExtension(filePath),
                    FileContent = fileBytes
                };

                fileMessage.Add(fileMessages);
                // Ваш код для отправки файла
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите файл.");
            }
            MessegeMail mail = new MessegeMail() { DateTime = DateTime.Now, Topic = Tbox.Text, MailMess = Mbox.Text, IdHow = Mail, IdWhom = Hbox.Text + MailAdress.Text, Files = fileMessage };
            string m = JsonSerializer.Serialize(mail);
            TCPClient tCPClient = new TCPClient();
            tCPClient.Tcpclient("Save data" + "`" + m);
            UpdateData();
        }
        public async void UpdateData()
        {
            try
            {
                while (true)
                {
                    GetData getData = new GetData();
                    var mail = JsonSerializer.Deserialize<MessegeMail[]>(await getData.GetDatasH(Mail));

                    OutMess.ItemsSource = mail.Where(c => c.IdWhom != Mail);
                    inMess.ItemsSource = mail.Where(c => c.IdHow != Mail);
         
                }
              
              
                
                
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

        private void inMess_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessegeMail? messegeMail = inMess.SelectedItem as MessegeMail;
                Outblock2.Text = messegeMail?.MailMess;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


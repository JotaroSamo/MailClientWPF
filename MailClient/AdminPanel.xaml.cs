using MailClient.Models.DBModels;
using MailClient.Network.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MailClient.Network;
using System.IO;

namespace MailClient
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        string Mail;
        Models.DBModels.User user;
        string perm;
        public AdminPanel(string Mail)
        {
            InitializeComponent();
            this.Mail = Mail;
       
            Task.Run(() => UpdateData());
            Task.Run(() => UpdateUsers());

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageMail? messegeMail = OutMess.SelectedItem as MessageMail;
            TCPClient tCPClient = new TCPClient();
            tCPClient.Tcpclient("Delete" + "`" + messegeMail?.Id);

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            MessageMail? messegeMail = OutMess.SelectedItem as MessageMail;
            if (messegeMail is MessageMail selectedMail)
            {

                // Здесь реализуйте логику скачивания файлов
                // Например, для первого файла в списке:
                var fileMessage = selectedMail.Files.ToList();
                if (fileMessage != null)
                {
                    foreach (var file in fileMessage)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = file.FileName;
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, file.FileContent);
                        }
                    }

                }
            }
        }
        private void Out(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private async Task UpdateUsers()
        {
            GetData getData = new GetData();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var repost = JsonSerializer.Deserialize<List<User>>(await getData.GetUsers(), options);



            // Для обеспечения обращения к элементам UI из главного потока:
            Dispatcher.Invoke(() =>
            {
                UsersGrid.ItemsSource = repost.ToList();

            });

            // Добавление задержки для предотврощения бесконечного цикла без ожидания
            await Task.Delay(10000);
        }
        private void OutMess_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessageMail? messegeMail = OutMess.SelectedItem as MessageMail;
                Outblock.Text = messegeMail?.MailMess;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную строку (предполагается, что в вашем классе есть свойство SelectedUser, куда привязан SelectedItem вашего DataGrid)
            User selectedUser = UsersGrid.SelectedItem as User;

            if (selectedUser != null)
            {
                EditUser editUser = new EditUser(selectedUser);
                editUser.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную строку (предполагается, что в вашем классе есть свойство SelectedUser, куда привязан SelectedItem вашего DataGrid)
            User selectedUser = UsersGrid.SelectedItem as User;

            if (selectedUser != null)
            {
                TCPClient tCPClient = new TCPClient();
                tCPClient.Tcpclient("/DeleteUser" + "`" + selectedUser?.Id);
            }
        }
        private void OutMess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutMess.SelectedItem is MessageMail selectedMail && selectedMail.Files != null && selectedMail.Files.Count > 0)
            {
                DownloadButton.IsEnabled = true;
            }
            else
            {
                DownloadButton.IsEnabled = false;
            }
        }
        private async Task UpdateData()
        {
            try
            {


                while (true)
                {
                    GetData getData = new GetData();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var repost = JsonSerializer.Deserialize<List<MessageMail>>(await getData.GetDatasH("all"), options);

                  

                    // Для обеспечения обращения к элементам UI из главного потока:
                    Dispatcher.Invoke(() =>
                    {
                        OutMess.ItemsSource = repost.ToList();

                    });

                    // Добавление задержки для предотврощения бесконечного цикла без ожидания
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                // Обработчик исключений
            }
        }
    }
}

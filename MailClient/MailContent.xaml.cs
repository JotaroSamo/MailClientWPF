using MailClient.DB.Content;
using MailClient.Models.DBModels;
using MailClient.Network;
using MailClient.Network.Methods;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public class FilesToEnabledConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var files = value as ICollection<FileMessage>;
                return files != null && files.Count > 0;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        string Mail;
        Models.DBModels.User user;
        string perm;
        //private BindingList<MessegeMail> messegeMail;
        public MailContent(string Mail)
        {

            InitializeComponent();
            this.Mail = Mail;
            send.Visibility = Visibility.Collapsed;
            Task.Run(() => UpdateData());

        }
        private List<string> filePaths = new List<string>(); // Список путей к файлам

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true // Разрешить выбор нескольких файлов
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePaths = openFileDialog.FileNames.ToList(); // Сохранение всех выбранных путей

                // Отображение выбранных файлов (может быть изменено для более удобного представления)
                fileName.Content = string.Join(", ", filePaths.Select(System.IO.Path.GetFileName));
            }
        }
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            List<FileMessage> fileMessages = new List<FileMessage>();

            foreach (var filePath in filePaths) // Обработка всех выбранных файлов
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                // Добавление файла в список
                fileMessages.Add(new FileMessage
                {
                    FileName = System.IO.Path.GetFileName(filePath),
                    FileType = System.IO.Path.GetExtension(filePath),
                    FileContent = fileBytes
                });
            }

 
                // Ваш код для отправки списка файлов
                MessageMail mail = new MessageMail
                {
                    DateTime = DateTime.Now,
                    Topic = Tbox.Text,
                    MailMess = Mbox.Text,
                    IdHow = Mail,
                    IdWhom = Hbox.Text + MailAdress.Text,
                    Files = fileMessages // Присвоение списка файлов сообщению
                };

                string m = JsonSerializer.Serialize(mail);
                TCPClient tCPClient = new TCPClient();
                Task.Run(() => tCPClient.Tcpclient("Save data" + "`" + m));
                filePaths.Clear();
                // Обновление интерфейса пользователя, возможно, очистка представления выбранных файлов
                fileName.Content = string.Empty;
            
      
        }
        private async void MailName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check checkUser = new Check();
         
            try
            {
                user = new Models.DBModels.User() { Mail = Hbox.Text + MailAdress.Text};
                perm = JsonSerializer.Serialize(user);
                bool b = await checkUser.Checks(perm, "AddUser", "");
                if (b == false)
                {
                    send.Visibility = Visibility.Visible;
                    nogood.Visibility = Visibility.Collapsed;
                    good.Visibility = Visibility.Visible;
                    good.Content = "Пользователь найден";
                }
                if (b == true)
                {
                    send.Visibility = Visibility.Collapsed;
                    good.Visibility = Visibility.Collapsed;
                    nogood.Visibility = Visibility.Visible;
                    nogood.Content = "Пользователь не нейден";
                }
            }
            catch (Exception)
            {


            }

        }
        public async Task UpdateData()
        {
            try
            {
                int prevCountInMess = -1; // Для хранения предыдущего числа входящих сообщений

                while (true)
                {
                    GetData getData = new GetData();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var repost = JsonSerializer.Deserialize<List<MessageMail>>(await getData.GetDatasH(Mail), options);

                    var inMessages = repost.Where(c => c.IdHow != Mail).ToList(); // Входящие сообщения

                    // Для обеспечения обращения к элементам UI из главного потока:
                    Dispatcher.Invoke(() =>
                    {
                        OutMess.ItemsSource = repost.Where(c => c.IdWhom != Mail).ToList();
                        inMess.ItemsSource = inMessages;

                        // Проверка, увеличилось ли количество входящих сообщений
                        if (prevCountInMess != -1 && inMessages.Count > prevCountInMess)
                        {

                            System.Media.SystemSounds.Exclamation.Play();
                        }

                        prevCountInMess = inMessages.Count; // Обновляем значение предыдущего количества сообщений
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
        private void inMess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inMess.SelectedItem is MessageMail selectedMail && selectedMail.Files != null && selectedMail.Files.Count > 0)
            {
                DownloadButton2.IsEnabled = true;
            }
            else
            {
                DownloadButton2.IsEnabled = false;
            }
        }
        private void DownloadButton2_Click(object sender, RoutedEventArgs e)
        {
            MessageMail? messegeMail = inMess.SelectedItem as MessageMail;
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

     

        private void Out(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            MessageMail? messegeMail = OutMess.SelectedItem as MessageMail;
            TCPClient tCPClient = new TCPClient();
           await tCPClient.Tcpclient("Delete" + "`" + messegeMail?.Id);
        
        }

        private void inMess_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessageMail? messegeMail = inMess.SelectedItem as MessageMail;
                Outblock2.Text = messegeMail?.MailMess;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


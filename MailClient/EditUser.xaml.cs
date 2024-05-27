using MailClient.Models.DBModels;
using MailClient.Network.Methods;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private Models.DBModels.User EditedUser;
        public EditUser(Models.DBModels.User user)
        {
            this.EditedUser = user;
            InitializeComponent();
            MailBox.Text = user.Mail;
            PasswordBox.Text = user.Passowrd;
            RoleComboBox.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>();

            // Установка выбранного элемента в ComboBox
            RoleComboBox.SelectedItem = user.Role;
        }
        
        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Обновление данных выбранного пользователя
            EditedUser.Mail = MailBox.Text;
            EditedUser.Passowrd= PasswordBox.Text;
            // Обновление роли в зависимости от выбранного значения в ComboBox
            EditedUser.Role = (Role)Enum.Parse(typeof(Role), RoleComboBox.SelectedItem.ToString());
            await SaveEdit();

            // Дальнейшие операции по редактированию пользователя...
        }
        private async Task SaveEdit()
        {
            SetData setData = new SetData();
            await setData.UpdateUser(this.EditedUser);
            this.Close();
        }
    }
}

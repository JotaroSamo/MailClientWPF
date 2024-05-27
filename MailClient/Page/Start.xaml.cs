using MailClient.Models.DBModels;
using MailClient.Network.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailClient.Page
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start 
    {
        User user;
        string perm;
        MainWindow main;
        public Start(MainWindow Main)
        {
            InitializeComponent();
            main = Main;
        }
        private async void Out_Click(object sender, RoutedEventArgs e)
        {
            Check checkUser = new Check();
            user = new User() { Mail = MailName.Text + NameMail.Text, Passowrd = PasswordText.Password };
            perm = JsonSerializer.Serialize(user);
            bool b = await checkUser.Checks(perm, "+","");
            if (b == true)
            {
                bool isAdmin = await checkUser.IsAdmin(perm, "/IsAdmin", "");
                if (isAdmin)
                {
                    AdminPanel adminPanel = new AdminPanel(MailName.Text + NameMail.Text);
                    adminPanel.Show();
                }
                else
                {
                    MailContent open = new MailContent(MailName.Text + NameMail.Text);
                    open.Show();
                    
                }
                main.Close();
            }
            if (b == false)
            {
                MessageBox.Show("Incorrect login or password!!!");
            }

        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new Register(main));
            
        }

       
    }
}

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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register 
    {
        MainWindow main;
        User user;
        string perm;
        public Register(MainWindow Main)
        {
            InitializeComponent();
            main = Main;

        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordText.Password==PasswordTextT.Password)
            {
                Check checkUser = new Check();
                user = new User() { Mail = MailName.Text + NameMail.Text, Passowrd = PasswordText.Password };
                perm = JsonSerializer.Serialize(user);
                checkUser.Checks(perm, "AddUser", "+");
                main.frame.Navigate(new Start(main));
            }
            else
            {
                MessageBox.Show("Password don`t match!");
            }
          
        }

        private void MailName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check checkUser = new Check();
            try
            {
                user = new User() { Mail = MailName.Text + NameMail.Text };
                perm = JsonSerializer.Serialize(user);
                bool b = checkUser.Checks(perm, "AddUser", "");
                if (b == true)
                {
                    Reg.Visibility = Visibility.Visible;
                    lb2.Visibility= Visibility.Collapsed;
                    lb.Visibility = Visibility.Visible;
                    lb.Content = "Good";
                }
                if (b == false)
                {
                    Reg.Visibility = Visibility.Collapsed;
                    lb.Visibility = Visibility.Collapsed;
                    lb2.Visibility = Visibility.Visible;
                    lb2.Content = "Not good!!!";
                }
            }
            catch (Exception)
            {

           
            }
          
        }
    }
}

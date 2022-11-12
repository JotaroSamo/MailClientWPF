using MailClient.Models.DBModels;
using MailClient.Network;
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


namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        string perm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            Check checkUser = new Check();
             user  = new User() {Mail= MailName.Text + NameMail.Text, Passowrd= PasswordText.Password };
            perm = JsonSerializer.Serialize(user);
            bool b = checkUser.Checks( perm,"+");
            if (b == true)
            {
                MailContent open = new MailContent(MailName.Text + NameMail.Text);
                open.Show();
                this.Close();
            }
            if (b == false)
            {
                MessageBox.Show("Incorrect login or password!!!");
            }

        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Check checkUser = new Check();
            user = new User() { Mail = MailName.Text + NameMail.Text, Passowrd = PasswordText.Password };
            perm = JsonSerializer.Serialize(user);
            bool b = checkUser.Checks(perm, "AddUser");
            if (b == true)
            {
                MessageBox.Show("Successful registration!!");
            }
            if (b == false)
            {
                MessageBox.Show("Such a user exists!");
            }
        }


     
    }
}

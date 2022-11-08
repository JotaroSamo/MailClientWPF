using MailClient.Network;
using MailClient.Network.Methods;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            Check checkUser = new Check();
            bool b = checkUser.Checks(MailName.Text+NameMail.Text, PasswordText.Password, "+");
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
            bool b = checkUser.Checks(MailName.Text + NameMail.Text, PasswordText.Password, "AddUser");
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

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
            bool b = checkUser.Checks(MailName.Text, PasswordText.Password, "+");
            if (b == true)
            {
                MailContent open = new MailContent(MailName.Text);
                open.Show();
                //this.Hide();
            }
            if (b == false)
            {
                MessageBox.Show("Не правельный логин или пароль!!!");
            }
            //MailContent open = new MailContent(MailName.Text);
            //open.Show();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Check checkUser = new Check();
            bool b = checkUser.Checks(MailName.Text, PasswordText.Password, "AddUser");
            if (b == true)
            {
                MessageBox.Show("Успешная регестрация!!!");
            }
            if (b == false)
            {
                MessageBox.Show("Такой польщователь существует!!!");
            }
        }
    }
}

using MailClient.Models.DBModels;
using MailClient.Network;
using MailClient.Network.Methods;
using MailClient.Page;
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
        MainWindow main;
        public MainWindow()
        {
            InitializeComponent();
            main=this;
            frame.Navigate(new Start(main)) ;
        }



     
    }
}

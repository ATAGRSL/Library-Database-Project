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

namespace aLibrary
{
    /// <summary>
    /// Interaction logic for adminmenu.xaml
    /// </summary>
    public partial class adminmenu : Window
    {
        public adminmenu()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            bookconfirm bookconfirm = new bookconfirm();
            bookconfirm.Show();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
            usersettings usersettings = new usersettings();
            usersettings.Show();
        }
    }
}

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
using Microsoft.Win32;

namespace aLibrary
{
    /// <summary>
    /// Interaction logic for menulist.xaml
    /// </summary>
    public partial class menulist : Window
    {
        public menulist()
        {
            InitializeComponent();
        }
        RegistryKey key;
        int id;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\aLibrary", true);// kayıt defterindeki var olan değer verisini açıyor
            label.Content+= " " + key.GetValue("nick").ToString();
            string b = key.GetValue("id").ToString();
            id = Convert.ToInt32(b);
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            searchbook searchbook = new searchbook();
            searchbook.Show();
        }
        private void return_Click(object sender, RoutedEventArgs e)
        {
            returnbooks returnbooks = new returnbooks();
            returnbooks.Show();
        }
        private void logout2_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }
        private void change_Click(object sender, RoutedEventArgs e)
        {
            account account = new account();    
            account.Show();
        }
        private void teacher_Click(object sender, RoutedEventArgs e)
        {
            int rank =Convert.ToInt32( dbop.sendquery("select * from usertbl where id="+id+"").Rows[0]["ranks"]);
            if (rank == 2 || rank==4) {
                return;
            }
            int a = dbop.sendexecute("update usertbl set ranks = 4 where id ="+id+"");
            if (a==-1)
            {
                MessageBox.Show("There is an error! Contact your admin!");
            }
            else
            {
                MessageBox.Show("REQUEST SENT.");
            }
        }
    }
}

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

namespace aLibrary
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

        private void save2_Click(object sender, RoutedEventArgs e)
        {
            int kullanicisayisi = dbop.sendquery("select * from usertbl where nick ='"+nick.Text+"'").Rows.Count;
            if (kullanicisayisi != 0)
            {
                resultlabel.Content = "Same Nick!";
                return;
            }
            string registerdate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            int result = dbop.sendexecute("insert into usertbl(nick,pass,regisdate,ranks) values('"+nick.Text+"'," +
                "'"+dbop.sifrele(passwordBox.Password)+"','"+registerdate+"',"+1+")");
            if (result!=-1)
            {
                resultlabel.Content = "Register done!";

            }
        }

        private void ahaa_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }
    }
}

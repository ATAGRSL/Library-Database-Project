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
    /// Interaction logic for account.xaml
    /// </summary>
    public partial class account : Window
    {
        public account()
        {
            InitializeComponent();
        }
        RegistryKey key;
        int id;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\aLibrary", true);// kayıt defterindeki var olan değer verisini açıyor
            label.Content += " " + key.GetValue("nick").ToString(); // nick adlı değeri alıyor label içine yazdırıyor.
            string a = key.GetValue("id").ToString();
            id = Convert.ToInt32(a);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string sqlpass = dbop.sendquery("select * from usertbl where id = " + id + "").Rows[0]["pass"].ToString();
            if (dbop.sifrele(textBox.Text)==sqlpass)
            {
                if (textBox1.Text==textBox12.Text)
                {
                    int a = dbop.sendexecute("update usertbl set pass ='"+dbop.sifrele(textBox12.Text)+"' where id ="+id+" ");
                    if (a==-1) // int türünde fonkiyon sonucu int return ediyor.
                    {
                        label2.Content = "There is an error! Contact your admin";
                    }
                    else
                    {
                        label2.Content = "YOUR NEW PASSWORDS HAVE BEEN UPDATED SUCCESSFULLY!";
                        textBox12.Text = "";
                        textBox.Text = "";  
                        textBox1.Text = "";  
                    }
                }
                else
                {
                    label2.Content = "YOUR NEW PASSWORDS IS INCORRECT!";
                }
            }
            else
            {
                label2.Content = "YOUR CURRENT PASSWORD IS INCORRECT!";
            }
        }
    }
}

using System;
using System.Data;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public static string srLoggedUserName;
        public static int irLoggedUserId;

        public login()
        {
            InitializeComponent();
        }

        private void dhaa_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void login2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text=="")
            {
                resultlabel.Content = "Fill textboxs!";
                return;
            }
            if (passwordBox.Password=="")
            {
                resultlabel.Content = "Fill passwordbox!";
                return;
            }

            DataTable varmi = dbop.sendquery("select * from usertbl where nick ='"+textBox.Text+"'");
            if (varmi.Rows.Count==0)
            {
                resultlabel.Content = "ACCOUNT NOT REGISTERED!!";
                return;
            }
            string haspass = dbop.sifrele(passwordBox.Password);
            string sqlpass = varmi.Rows[0]["pass"].ToString();
            if (haspass == sqlpass)
            {
                srLoggedUserName = textBox.Text;
              Int32.TryParse(varmi.Rows[0]["id"].ToString(), out irLoggedUserId);  
                int rank = (int)varmi.Rows[0]["ranks"];
                if (rank==1 || rank == 2 || rank == 4)
                {
                    RegistryKey key;
                    key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\aLibrary", true); // kayıt defterindeki var olan değer verisini açıyor
                    if (key!=null) // boş değilse var olanın üzerine yazar
                    {
                        key.SetValue("nick", textBox.Text); 
                        key.SetValue("id", varmi.Rows[0]["id"].ToString());
                    
                    }
                    else
                    {
                        key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\aLibrary"); // yeni bir veri dosyası oluşturur
                        System.IO.File.Create("reg.aLibrary"); 
                        key.SetValue("nick", textBox.Text);
                        key.SetValue("id", varmi.Rows[0]["id"].ToString());

                    }
                    menulist menulist = new menulist();
                    menulist.Show();
                    this.Close();
                }else if (rank == 3)
                {
                    adminmenu adminmenu = new adminmenu();
                    adminmenu.Show();
                }
            }
            else
            {
                resultlabel.Content = "WRONG PASS!";

            }
        }
    }
}

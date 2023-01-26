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
    /// Interaction logic for searchbook.xaml
    /// </summary>
    public partial class searchbook : Window
    {
        public searchbook()
        {
            InitializeComponent();
        }
        RegistryKey key;
        int id;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\aLibrary", true);// kayıt defterindeki var olan değer verisini açıyor
            string a =key.GetValue("id").ToString();
            id =Convert.ToInt32(a);
            dataGrid.ItemsSource = dbop.sendquery("select * from booktbl where status = 0 order by id").AsDataView();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = dbop.sendquery("select * from booktbl where status = 0 and ad like '%"+textBox.Text+"%' order by id").AsDataView();

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = dbop.sendquery("select * from booktbl where status = 0 and yazar like '%" + textBox1.Text + "%' order by id").AsDataView();
        }
        private void take_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem==null)
            {
                return;
            }
            DataRowView kitap = dataGrid.SelectedItem as DataRowView;
            MessageBoxResult sonuc = MessageBox.Show("Are you sure take this book? " + kitap.Row["ad"], "Confirmation", MessageBoxButton.YesNo);
            if (sonuc==MessageBoxResult.Yes)
            {
                DataTable kitapsayi = dbop.sendquery("select * from log where userid ="+id+" and (status=1 or status=0)");
                if (kitapsayi.Rows.Count>4)
                {
                    resultlabel.Content = "You can not take books anymore";
                    return;
                }
                int a = dbop.sendexecute("insert into log(kitapid,userid,status) " +
                    "values('"+ kitap.Row["id"]+ "',"+ id + ","+0+")");
                if (a==-1)
                {
                    resultlabel.Content = "There is an error! Contact your admin";
                }
                else
                {
                    resultlabel.Content = "You have successfully borrowed the book! The Book Name: " + kitap.Row["ad"];
                    dbop.sendexecute("update booktbl set status=1 where id ="+kitap.Row["id"]+"");
                    dataGrid.ItemsSource = dbop.sendquery("select * from booktbl where status = 0 order by id").AsDataView();
                }
            }

        }

        private void textBox12_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = dbop.sendquery("select * from booktbl where status = 0 and category like '%" + textBox12.Text + "%' order by id").AsDataView();
        }
    }
}

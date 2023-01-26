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
using System.Data;
namespace aLibrary
{
    /// <summary>
    /// Interaction logic for bookconfirm.xaml
    /// </summary>
    public partial class bookconfirm : Window
    {
        public bookconfirm()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem==null)
            {
                return;
            }
            DataRowView kitap = dataGrid.SelectedItem as DataRowView;
            MessageBoxResult sonuc = MessageBox.Show("Confirm this book? ", "Confirmation", MessageBoxButton.YesNo);
            if (sonuc == MessageBoxResult.Yes)
            {
                int a = dbop.sendexecute("update log set status = 2 where id ="+kitap.Row["id"]+"");
                if (a==-1) // int türünde fonkiyon sonucu int return ediyor. 
                {
                    label.Content = "There is an error! Contact your admin!";
                }
                else
                {
                    label.Content = "YOU HAVE SUCCESSFULLY CONFIRMED THE REQUEST!";
                    dbop.sendexecute("update booktbl set status = 0 where id = " + kitap.Row["kitapid"] + "");
                }
                dataGrid.ItemsSource = dbop.sendquery("select * from log where status = 1").AsDataView();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbop.sendquery("select * from log where status = 1").AsDataView(); 
            // datatable return ediyor dataviewe çeviriyorum datagride yüklüyor
        }
    }
}

using System;
using System.Data;
using Microsoft.Win32;
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
    /// Interaction logic for returnbooks.xaml
    /// </summary>
    public partial class returnbooks : Window
    {
        public returnbooks()
        {
            InitializeComponent();
        }
        RegistryKey key;
        int id;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
    
            label.Content += " " + login.srLoggedUserName;

            id = login.irLoggedUserId;
            dataGrid.ItemsSource = dbop.sendquery("select l.id,l.kitapid,k.ad from log l inner join booktbl k on k.id = l.kitapid where l.status = 0 and l.userid ="+ id + " ").AsDataView();
        }

        private void returnbook_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                return;
            }
            DataRowView kitap = dataGrid.SelectedItem as DataRowView;
            MessageBoxResult sonuc = MessageBox.Show("Return this book? " + kitap.Row["ad"], "Confirmation", MessageBoxButton.YesNo);
            if (sonuc == MessageBoxResult.Yes)
            {
                int a = dbop.sendexecute("update log set status = 1 where id="+kitap.Row["id"]+"");
                if (a==-1) // int türünde fonkiyon sonucu int return ediyor. 
                {
                    label1.Content = "There is an error! Contact your admin";
                }
                else
                {
                    label1.Content = "You have successfully returned it!";
                    dataGrid.ItemsSource = dbop.sendquery("select l.id,l.kitapid,k.ad from log l inner join booktbl k on k.id = l.kitapid where l.status = 0 and l.userid =" + id + " ").AsDataView();
                }
            }
        }

       
    }
}

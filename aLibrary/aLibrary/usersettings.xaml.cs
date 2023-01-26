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

namespace aLibrary
{
    /// <summary>
    /// Interaction logic for usersettings.xaml
    /// </summary>
    public partial class usersettings : Window
    {
        public usersettings()
        {
            InitializeComponent();
        }

        private void confirm2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem==null)
            {
                return;
            }
            DataRowView id = dataGrid.SelectedItem as DataRowView;
            MessageBoxResult sonuc = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButton.YesNo);
            if (sonuc == MessageBoxResult.Yes)
            {
                int a = dbop.sendexecute("update usertbl set ranks=2 where id ="+id.Row["id"]+"");
                if (a==-1) // int türünde fonkiyon sonucu int return ediyor. 
                {
                    label.Content = "There is an error! Contact your admin";
                }
                else
                {
                    label.Content = "YOU HAVE SUCCESSFULLY CONFIRMED THE REQUEST!";
                    dataGrid.ItemsSource = dbop.sendquery("select * from usertbl where ranks = 4").AsDataView();

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbop.sendquery("select * from usertbl where ranks = 4").AsDataView();
        }
    }
}

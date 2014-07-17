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
namespace Tess
{
    /// <summary>
    /// Interaction logic for Dictionary.xaml
    /// </summary>
    public partial class Dictionary : Window
    {
        public Dictionary()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            Connection c = new Connection();
            DataTable dt = c.FillTable("Select * from Slownik");
        //    MessageBox.Show(dt.Rows.Count.ToString());
            dictionaryDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(addEntryTextBox.Text!="")
            {
                Connection c = new Connection();
                c.NonQuery("INSERT INTO Slownik VALUES ('"+addEntryTextBox.Text+"') ");
                addEntryTextBox.Text = "";
                FillDataGrid();
                c.Dispose();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Connection c = new Connection();
            c.NonQuery("DELETE FROM Slownik");
            
            FillDataGrid();
            c.Dispose();
        }
    }
}

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

namespace OOOSportProduct
{
    /// <summary>
    /// Логика взаимодействия для OrderDescription.xaml
    /// </summary>
    public partial class OrderDescription : Window
    {
        public OrderDescription()
        {
            InitializeComponent();
        }

        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }
    }
}

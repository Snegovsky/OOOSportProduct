using OOOSportProduct.Classes;
using OOOSportProduct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
        }

        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }
        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderDescription orderDescription = new OrderDescription();
            orderDescription.Show();
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helper.User != null) 
            { 
                tbFIO.Text = Helper.User.UseSurname  +" "+ Helper.User.UserName + " " + Helper.User.UserLastname;
            }
            else 
            {
                tbFIO.Text = "Гость";
            }
            cbDiscount.Items.Clear();
            cbDiscount.Items.Add("Все диапазоны");
            cbDiscount.Items.Add("0-9,99%");
            cbDiscount.Items.Add("10-14,99%");
            cbDiscount.Items.Add("15% и более");
            cbDiscount.SelectedIndex = 0;	
            
            List<Category> categories = Helper.DB.Category.ToList();
            
            Category allCategory = new Category();
            allCategory.CategoryID = 0;
            allCategory.CategoryName = "Все категории";
            categories.Insert(0, allCategory);
            cbCategory.ItemsSource = categories;	
            
            cbCategory.DisplayMemberPath = "CategoryName";
            cbCategory.SelectedValuePath = "CategoryId";
            cbCategory.SelectedIndex = 0;
            cbCategory.SelectedValue = 0;
            rbSortAsc.IsChecked = true;		
            ShowProduct();

            buttonOrder.Visibility = Visibility.Hidden;
            if (Helper.User == null || Helper.User.UserRole == 1)
            {
                buttonOrderEdit.Visibility = Visibility.Hidden;
                buttonProductAdd.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Helper.User.UserRole == 2)
                {
                    buttonOrderEdit.Visibility = Visibility.Visible;
                }
                else
                {
                    buttonOrderEdit.Visibility = Visibility.Visible;
                    buttonProductAdd.Visibility = Visibility.Visible;
                }
            }
            
        }

        /// <summary>
        /// Отображает данные о товарах с учетом выбранных параметров
        /// </summary>
        private void ShowProduct()
        {
           
            List<Model.Product> products = new List<Model.Product>();
            products = Helper.DB.Product.ToList();
            int totalCount = products.Count;		
            
            double maxDiscount = 100;
            double minDiscount = 0;
            switch (cbDiscount.SelectedIndex)		
            {
                case 1:
                    maxDiscount = 9.99;
                    break;
                case 2:
                    minDiscount = 10;
                    maxDiscount = 14.99;
                    break;
                case 3:
                    minDiscount = 15;
                    break;
            }

            //Осуществить фильтрации по выбранной скидке
            products = products.Where(pr => pr.ProductMaxSale <= maxDiscount &&
                                                         pr.ProductMaxSale >= minDiscount).ToList();
            //Сортировка от состояния радиокнопки
            if ((bool)rbSortAsc.IsChecked)		//По возрастанию
            {
                products = products.OrderBy(pr => pr.ProductCost).ToList();
            }
            else
            {
                products = products.OrderByDescending(pr => pr.ProductCost).ToList();
            }
            //Фильтрация по категории в случае, если не все категории
            if (cbCategory.SelectedIndex > 0)
            {
                products = products.Where(pr => pr.ProductCategory == cbCategory.SelectedIndex).ToList();
            }
            //Поиск по названию
            string search = tbSearch.Text;
            search = search.ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                 products = products.Where(pr => pr.ProductName.ToLower().Contains(search)).ToList();

            }
            
            //Отображение после выполнения всех фильтраций
            int filterCount = products.Count;	
            tbCount.Text = filterCount.ToString() + " из " + totalCount.ToString();
            //listBoxProducts.ItemsSource = products;

            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            foreach (Model.Product product in products)	
            {
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                productExtended.Product = product;
                productExtendeds.Add(productExtended);
            }

            listBoxProducts.ItemsSource = productExtendeds;
        }

        
        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void rbSortAsc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProduct();
        }

        private void rbSortDesc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProduct();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProduct();
        }

        private void cmAddInOrder_Click(object sender, RoutedEventArgs e)
        {
            buttonOrder.Visibility = Visibility.Visible;
        }

        private void buttonOrderEdit_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Close();
        }
    }
}

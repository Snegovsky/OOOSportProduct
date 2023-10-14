using OOOSportProduct.Model;
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


namespace OOOSportProduct
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        /// <summary>
        /// Завершение работы приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //подключение к БД
            Helper.DB = new DBSportProducts();

        }
        /// <summary>
        /// Обработка логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Password;
            StringBuilder sb = new StringBuilder();
            if (login =="")
            {
                sb.Append("Вы не ввели логин.\n");
            }
            if (password =="")
            {
                sb.Append("Вы не ввели пароль.\n");
            }
            if (sb.Length > 0) 
            {
                //MessageBox.Show(sb.ToString());            
            }
            ////работа с БД
            //List<Model.User> users = new List<Model.User>();
            ////Все записи таблицы User
            //users = Helper.DB.User.ToList();
            ////Получить первую запись из списка
            //Model.User user = users.Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            List<Model.User> users = new List<Model.User>();
            users = Helper.DB.User.ToList();
            Helper.User = users.Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            if (Helper.User != null)
            {
                //Доступ к конкретному полю
                string userName = Helper.User.UserName;
                int userRoleID = Helper.User.UserRole;
                //Доступ по навигационному свойству к полю связанной таблицы
                string userRoleName = Helper.User.Role.RoleName;
                MessageBox.Show(userName + "\nКод роли: " + userRoleID + "\nНазвание роли: " + userRoleName);

            }
            else
            {
                MessageBox.Show("Вы не зарегистрированы в системе");
            }

        }
    }
}

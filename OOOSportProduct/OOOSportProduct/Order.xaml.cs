using OOOSportProduct.Classes;
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
using System.Windows.Shapes;

namespace OOOSportProduct
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {

        //Список всех заказов из БД
        List<Model.Order> listOrders = new List<Model.Order>();
        //Список заказанных товаров
        List<Model.OrderProduct> listProductsInOrders = new List<Model.OrderProduct>();
        //Список заказанных товаров с дополнительными свойствами
        List<Classes.OrderExtended> listExtendedOrders;

        Classes.OrderExtended selectOrder;		//Выбранный заказ


        public Order()
        {
            InitializeComponent();
        }
        private void ShowOrder()
        {
            listOrders = Helper.DB.Order.ToList();	//Получить все заказы
            int totalCount = listOrders.Count;		//Их общее количество
            listProductsInOrders = Helper.DB.OrderProduct.ToList(); //Получить все заказанные товары
            //Создание списка заказанных товаров с расширенными свойствами
            listExtendedOrders = new List<OrderExtended>();
            
            foreach (var order in listOrders)
            {
                Classes.OrderExtended orderExtended = new OrderExtended();
                orderExtended.order = order;		//Заполнить данные о заказе из БД
                //Вызов методов класса через объект для вычисления дополнительных свойств
                //orderExtended.SumOrder = orderExtended.CalcSummaOrder(listProductsInOrders);
                //orderExtended.SumOrderWithDiscount = orderExtended.CalcSummaDiscountPercent(listProductsInOrders);
                listExtendedOrders.Add(orderExtended);
            }
            //Сортировка
            switch (cbSort.SelectedIndex)
            {
                case 1:
                    listExtendedOrders = listExtendedOrders.OrderBy(or => or.SumOrder).ToList();
                    break;
                case 2:
                    listExtendedOrders =
                                       listExtendedOrders.OrderByDescending(or => or.SumOrder).ToList();
                    break;
            }
            //Отображение информации о заказах
            int filterCount = listExtendedOrders.Count;
            listViewOrders.ItemsSource = null;
            listViewOrders.ItemsSource = listExtendedOrders;
            tbCount.Text = filterCount + " из " + totalCount;
        }

        /// Метод создает список заказанных товаров только для конкретного заказа
        /// <param name="orderId"> Номер заказа</param>
        /// <returns>Построенный список из товаров в заказе</returns>
        public List<Model.OrderProduct> ListProductsInOrderId(int orderId)
        {
            List<Model.OrderProduct> list = new List<Model.OrderProduct>();
            //Поиск всех товаров для выбранного номера заказа
            list = listProductsInOrders.FindAll(pr => pr.OrderID == orderId).ToList();
            return list;
        }


    

        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        /// При загрузке окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Список статусов заказа
            cbStatus.ItemsSource = Helper.DB.OrderStatus.ToList();
            cbStatus.DisplayMemberPath = "StatusName";
            cbStatus.SelectedValuePath = "StatusId";
            cbSort.SelectedIndex = 0;
            cbFilterDiscount.SelectedIndex = 0;
            ShowOrder();

        }


        /// Выбор заказа в списке заказов
        private void listViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Проверка, что есть выбранный заказ
            if (listViewOrders.SelectedItem == null)
                return;
            //Получение всей информации о выбранном заказе
            selectOrder = listViewOrders.SelectedItem as Classes.OrderExtended;
            //Формирование списка товаров в этом заказе по разработанному методу
            List<Model.OrderProduct> listTemp = ListProductsInOrderId(selectOrder.order.OrderID);
            //Отображение товаров выбранного заказа
            listViewOrder.ItemsSource = listTemp;
            //Отобразить статус заказа
            cbStatus.SelectedValue = selectOrder.order.OrderStatus;
            //Отобразить дату доставки заказа
            dateDelivery.SelectedDate = selectOrder.order.OrderDeliveryDate;
            ShowOrder();

        }

        /// Выбор направления сортировки
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowOrder();
        }

        /// Выбор условия фильтрации
        private void cbFilterDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowOrder();
        }

        /// Сохранение в таблицу Order БД изменений о заказе
        private void butEditOrder_Click(object sender, RoutedEventArgs e)
        {
            //Есть выбранный заказ для редактирования
            if (selectOrder != null)
            {
                //Получить объект модели заказа
                Model.Order order = selectOrder.order;
                //Задать новые значения для даты доставки и статуса
                order.OrderDeliveryDate = (DateTime)dateDelivery.SelectedDate;
                order.OrderStatus = (int)cbStatus.SelectedValue;
                try
                {
                    Helper.DB.SaveChanges();		//Сохранение в БД
                    MessageBox.Show("Данные успешно обновлены");
                    ShowOrder();
                }
                catch
                {
                    MessageBox.Show("При обновлении данных возникли проблемы");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не выбран редактируемый заказ");
            }
        }
    }

}




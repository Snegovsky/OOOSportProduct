using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSportProduct.Classes
{
    public class OrderExtended
    {
        //данные о заказе
        public Model.Order order { get; set; }

        //сумма всего заказа
        public double SumOrder { get; set; }

        //сумма скидки всего заказа
        public double SumOrderWithDiscount { get; set; }

        //общая скидка в %
        public double SumDiscountPercent { get; set; }
        //Classes.ProductExtended productExtended
        public double CalcSummaOrder(Classes.OrderExtended orderExtended)
        { 
        return SumOrder;
        }
        public double CalcSummaDiscountPercent() 
        {
            return SumOrderWithDiscount;
        }
    }
}

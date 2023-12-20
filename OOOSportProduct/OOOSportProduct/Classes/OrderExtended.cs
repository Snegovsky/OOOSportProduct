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

        public decimal CalcSummaOrder(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrder = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                SumOrder += Convert.ToDouble(orderProduct.Product.ProductCost * orderProduct.ProductCount);
            }

            return (decimal)SumOrder;
        }

        public decimal CalcSummaDiscountPercent(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrderWithDiscount = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                decimal productSaleDecimal = (decimal)orderProduct.Product.ProductSale;
                decimal discountAmount = orderProduct.Product.ProductCost * (productSaleDecimal / 100.0m);
                decimal priceWithDiscount = orderProduct.Product.ProductCost - discountAmount;
                SumOrderWithDiscount += Convert.ToDouble(priceWithDiscount * orderProduct.ProductCount);
            }

            return (decimal)SumOrderWithDiscount;
        }
    }
}

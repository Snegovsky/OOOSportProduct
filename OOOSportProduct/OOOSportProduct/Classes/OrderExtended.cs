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

        public double CalcSummaOrder(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrder = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                SumOrder += Convert.ToDouble(orderProduct.Product.ProductCost * orderProduct.ProductCount);
            }

            return SumOrder;
        }

        public double CalcSummaDiscountPercent(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrderWithDiscount = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                double productSaleDecimal = orderProduct.Product.ProductSale;
                double productCost = Convert.ToDouble(orderProduct.Product.ProductCost);
                productSaleDecimal /= 100.0;
                double discountAmount = productCost * productSaleDecimal;
                double priceWithDiscount = productCost - discountAmount;
                SumOrderWithDiscount += (priceWithDiscount * orderProduct.ProductCount);
                
            }

            return SumOrderWithDiscount;
        }
    }
}

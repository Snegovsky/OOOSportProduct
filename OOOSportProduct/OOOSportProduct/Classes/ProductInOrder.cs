using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSportProduct.Classes
{
    /// <summary>
    /// Описывает позицию в заказе
    /// </summary>
    public class ProductInOrder
    {
        public Classes.ProductExtended ProductExtended { get; set; }
        public int countProductInOrder { get; set; }

        public ProductInOrder(Classes.ProductExtended productExtended) 
        {
            ProductExtended = productExtended;
            countProductInOrder = 1;
        }
        
    }
}

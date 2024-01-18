using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OOOSportProduct.Classes
{
    public class ProductExtended
    {
        public Model.Product Product { get; set; }
        public string ProductPathPhoto
        {
            get 
            {
                string temp;
                if (! String.IsNullOrEmpty(this.Product.ProductPhoto))
                {
                    temp = Directory.GetCurrentDirectory() + "/images/" + this.Product.ProductPhoto;
                }
                else
                {
                    temp = @"\Resources\picture.png";
                }
                return temp;
            }
        }
        //private double productCostWithDiscount;
        public double ProductCostWithDiscount
        {
            get
            {
                double discountAmount = (double)this.Product.ProductCost*(Product.ProductSale/100.0);
                double priceWithDiscount = (double)Product.ProductCost - discountAmount;
                return priceWithDiscount;

            }
            set { this.ProductCostWithDiscount = value;}
        }

        public Visibility ProductCostDiscountVisibility
        { 
            get
            {
                Visibility result = Visibility.Collapsed;
                if (Product.ProductSale > 0) 
                { 
                    result = Visibility.Visible;
                }
                return result;
            } 
        }

        public SolidColorBrush ColorFocused
        {
            get 
            {
                SolidColorBrush result = new SolidColorBrush();
                result.Color = Color.FromArgb(255, 255, 255, 255);
                if (Product.ProductMaxSale > 15)
                {
                    result.Color = Color.FromArgb(0xFF, 0xCC, 0x66, 0x00);
                }
                return result;
            }
        }
    }
}

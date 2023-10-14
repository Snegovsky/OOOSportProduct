using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOOSportProduct.Model;

namespace OOOSportProduct.Model
{
    internal class Helper
    {
        //обьявление обьекта связи с БД
       public static Model.DBSportProducts DB { get; set; }
       public static Model.User User { get; set; }
    }
}

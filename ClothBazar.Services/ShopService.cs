using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ShopService
    {
        #region Singleton
        public static ShopService ClassObject
        {
            get
            {
                if (privetInMemoryObject == null)
                {
                    privetInMemoryObject = new ShopService();
                }
                return privetInMemoryObject;
            }
        }

        private static ShopService privetInMemoryObject { get; set; }      //Singleton Design Pattern For Pagenation

        private ShopService()
        {

        }
        #endregion

        public int SaveOrder(Order order)
        {
            var dbcontext = new CBContext();
            dbcontext.Orders.Add(order);
            return dbcontext.SaveChanges();
        }
    }
}

using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService ClassObject
        {
            get
            {
                if (privetInMemoryObject == null)
                {
                    privetInMemoryObject = new ConfigurationsService();
                }
                return privetInMemoryObject;
            }
        }

        private static ConfigurationsService privetInMemoryObject { get; set; }      //Singleton Design Pattern For Pagenation
        private ConfigurationsService()
        {

        }
        #endregion

        public Config GetConfig(string Key)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Configurations.Where(c => c.Key == Key).FirstOrDefault();
                //return dbcontext.Configurations.Find(Key);
            }
        }
        public int PageSize() 
        { 
            using (var dbcontext = new CBContext())
            {
                var pageSizeConfig = dbcontext.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 3;
            }
        }
    }
}

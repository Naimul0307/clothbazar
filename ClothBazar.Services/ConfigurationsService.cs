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
        public Config GetConfig(string Key)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Configurations.Where(c=>c.Key==Key).FirstOrDefault();
            }
        }
    }
}

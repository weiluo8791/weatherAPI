using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WeatherServiceHW04.Models
{
    public class WeatherServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        //private const string ConnectionStringName = "Name=MS_TableConnectionString";

        public WeatherServiceContext() : base("name=WeatherServiceHW04Context")
        //public WeatherServiceContext() : base(ConnectionStringName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Pressure> Pressures { get; set; }
        public DbSet<Humidity> Humidities { get; set; }
    }
}

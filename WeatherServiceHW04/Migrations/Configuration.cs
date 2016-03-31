using System.Globalization;
using System.IO;
using WeatherServiceHW04.Models;

namespace WeatherServiceHW04.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeatherServiceHW04.Models.WeatherServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeatherServiceHW04.Models.WeatherServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
             
            var allDays = File.ReadAllLines("c:\\MABOSTON.txt").Select(a => a.Split(','));
            var currentCulture = CultureInfo.CurrentCulture;
            foreach (var eachday in allDays)
            {
                DateTime thisDate = new DateTime(Int32.Parse(eachday[2]), Int32.Parse(eachday[0]), Int32.Parse(eachday[1]));
                context.Temperatures.AddOrUpdate(x => x.Id,
                new Temperature()
                {
                    Id = Guid.NewGuid().ToString(),
                    Degree = decimal.Parse(eachday[3]),
                    RecorDateTime = thisDate,
                    Year = thisDate.Year,
                    Month = thisDate.Month,
                    Week = currentCulture.Calendar.GetWeekOfYear(
                                thisDate,
                                currentCulture.DateTimeFormat.CalendarWeekRule,
                                currentCulture.DateTimeFormat.FirstDayOfWeek),
                    Day = thisDate.DayOfYear
                });

            }
        }
    }
}

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
            //load from file c:\MABOSTON.txt which contains tempature data for Boston from 1995-2016
            //initial loaded the temperature table
            var allDays = File.ReadAllLines("c:\\MABOSTON.txt").Select(a => a.Split(','));
            var currentCulture = CultureInfo.CurrentCulture;
            foreach (var eachday in allDays)
            {
                DateTime thisDate = new DateTime(int.Parse(eachday[2]), int.Parse(eachday[0]), int.Parse(eachday[1]));
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

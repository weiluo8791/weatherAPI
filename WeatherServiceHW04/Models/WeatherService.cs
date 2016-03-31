using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace WeatherServiceHW04.Models
{
    /// <summary>
    /// 
    /// </summary>
    [CustomValidation(typeof(Temperature), "ValidateDegree")]
    public class Temperature
    {
        public string Id { get; set; }
        [Required]
        public decimal? Degree { get; set; }
        [Required]
        public DateTime RecorDateTime { get; set; }
        [JsonIgnore]
        public int Year { get; set; }
        [JsonIgnore]
        public int Month { get; set; }
        [JsonIgnore]
        public int Week { get; set; }
        [JsonIgnore]
        public int Day { get; set; }

        public static ValidationResult ValidateDegree(Temperature temperature, ValidationContext ctx)
        {
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CustomValidation(typeof(Pressure), "ValidateMillibar")]
    public class Pressure
    {

        public string Id { get; set; }
        [Required]
        public decimal? Millibar { get; set; }
        [Required]
        public DateTime RecorDateTime { get; set; }
        [JsonIgnore]
        public int Year { get; set; }
        [JsonIgnore]
        public int Month { get; set; }
        [JsonIgnore]
        public int Week { get; set; }
        [JsonIgnore]
        public int Day { get; set; }

        public static ValidationResult ValidateMillibar(Pressure pressure, ValidationContext ctx)
        {
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CustomValidation(typeof(Humidity), "ValidatePercentage")]
    public class Humidity
    {

        public string Id { get; set; }
        [Required]
        public decimal? Percentage { get; set; }
        [Required]
        public DateTime RecorDateTime { get; set; }
        [JsonIgnore]
        public int Year { get; set; }
        [JsonIgnore]
        public int Month { get; set; }
        [JsonIgnore]
        public int Week { get; set; }
        [JsonIgnore]
        public int Day { get; set; }

        public static ValidationResult ValidatePercentage(Humidity humidity, ValidationContext ctx)
        {
            return ValidationResult.Success;
        }
    }
}
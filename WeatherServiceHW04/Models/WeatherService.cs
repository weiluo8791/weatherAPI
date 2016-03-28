using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WeatherServiceHW04.Models
{
    /// <summary>
    /// 
    /// </summary>
    [CustomValidation(typeof(Temperature), "ValidateDegree")]
    public class Temperature
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        /// <value>The name.</value>
        public decimal? Degree { get; set; }
        /// <summary>
        /// Gets or sets the customer's email address.
        /// </summary>
        /// <value>The email address.</value>
        public DateTime RecorDateTime { get; set; }

        /// <summary>
        /// Validates the name and email.
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="ctx">The context.</param>
        /// <returns>ValidationResult.</returns>
        public static ValidationResult ValidateDegree(Temperature temperature, ValidationContext ctx)
        {
            return ValidationResult.Success;
        }
    }

    [CustomValidation(typeof(Pressure), "ValidateMillibar")]
    public class Pressure
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        /// <value>The name.</value>
        public decimal? Millibar { get; set; }
        /// <summary>
        /// Gets or sets the customer's email address.
        /// </summary>
        /// <value>The email address.</value>
        public DateTime RecorDateTime { get; set; }

        /// <summary>
        /// Validates the name and email.
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name="ctx">The context.</param>
        /// <returns>ValidationResult.</returns>
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
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        /// <value>The name.</value>
        public decimal? Percentage { get; set; }
        /// <summary>
        /// Gets or sets the customer's email address.
        /// </summary>
        /// <value>The email address.</value>
        public DateTime RecorDateTime { get; set; }

        /// <summary>
        /// Validates the name and email.
        /// </summary>
        /// <param name="humidity"></param>
        /// <param name="ctx">The context.</param>
        /// <returns>ValidationResult.</returns>
        public static ValidationResult ValidatePercentage(Humidity humidity, ValidationContext ctx)
        {
            return ValidationResult.Success;
        }
    }
}
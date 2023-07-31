using System;
using System.Collections.Generic;
using System.Text;

namespace NutriGuide.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public string DateRecorded { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Nutrient { get; set; }

        public string LunchTitle { get; set; }
        public string LunchDescription { get; set; }
        public string LunchHours { get; set; }
        public string LunchNutrient { get; set; }

        public string DinnerTitle { get; set; }
        public string DinnerDescription { get; set; }
        public string DinnerHours { get; set; }
        public string DinnerNutrient { get; set; }
        public object UserId { get; internal set; }
    }
}

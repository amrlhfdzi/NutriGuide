using System;
using System.Collections.Generic;
using System.Text;

namespace NutriGuide.Models
{
    public class Meal
    {
        public string Id { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Image { get; set; }

        public string Benefit { get; set; }

        public string Disadvantage { get; set; }

        public string MealName { get; set; }

        public string Image2 { get; set; }

        public string Ingredient { get; set; }

        public string Instruction { get; set; }

        public List<string> Images { get; set; }
    }
}

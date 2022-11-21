using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Ingredient
    {
        [MinLength(1), Required]
        public string name { get; set; }
        [Range(1, int.MaxValue), Required]
        public int amount { get; set; }
        public string unit { get; set; }

        public Ingredient(string name, int amount, string unit)
        {
            this.name = name;
            this.amount = amount;
            this.unit = unit;
        }

        public Ingredient()
        {
        }
        
    }
}
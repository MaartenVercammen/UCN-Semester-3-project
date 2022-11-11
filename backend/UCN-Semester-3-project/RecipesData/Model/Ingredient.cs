using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Ingredient
    {
        public Recipe recipe { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string unit { get; set; }

        public Ingredient(Recipe recipe, string name, int amount, string unit)
        {
            this.recipe = recipe;
            this.name = name;
            this.amount = amount;
            this.unit = unit;
        }

        public Ingredient()
        {
        }
        
    }
}
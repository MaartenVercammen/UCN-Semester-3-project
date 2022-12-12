using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace admin_client.MVVM.Model
{
    public class Ingredient
    {
        public string name { get; set; }
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
            name = "";
            unit = "";
        }
        
    }
}
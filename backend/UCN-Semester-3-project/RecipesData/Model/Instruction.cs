using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Instruction
    {
        [Range(0, int.MaxValue),Required]
        public int Step {get;set;}
        [MinLength(5), Required]
        public string Description {get;set;}

        public Instruction(int step, string description)
        {
            this.Step = step;
            this.Description = description;
        }

        public Instruction()
        {
        }
        
    }
}
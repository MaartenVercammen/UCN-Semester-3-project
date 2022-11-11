using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Instruction
    {
        public Recipe Recipe {get;set;}
        public int Step {get;set;}
        public string Description {get;set;}

        public Instruction(Recipe recipe,int step,string description)
        {
            this.Recipe = recipe;
            this.Step = step;
            this.Description = description;
        }

        public Instruction()
        {
        }
        
    }
}

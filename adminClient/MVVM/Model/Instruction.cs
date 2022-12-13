using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminClient.MVVM.Model
{
    public class Instruction
    {
        public int Step {get;set;}
        public string Description {get;set;}

        public Instruction(int step, string description)
        {
            this.Step = step;
            this.Description = description;
        }

        public Instruction()
        {
            Description = "";
        }
        
    }
}
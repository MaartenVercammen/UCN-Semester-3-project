using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class List
    {
        public User user { get; set; }
        public Recipe recipe { get; set; }
        public bool isLiked { get; set; }

        public List(User user, Recipe recipe, bool isLiked)
        {
            this.user = user;
            this.recipe = recipe;
            this.isLiked = isLiked;
        }

        public List()
        {
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Recipe
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string PictureURL { get; set; }
        public int Time { get; set; }
        public int PortionNum { get; set; }

        public Recipe(string recipeId, string name, string description, string authorId, string pictureURL, int time, int portionNum)
        {
            this.RecipeId = recipeId;
            this.Name = name;
            this.Description = description;
            this.AuthorId = authorId;
            this.PictureURL = pictureURL;
            this.Time = time;
            this.PortionNum = portionNum;
        }

        public Recipe()
        {
        }

    }
}

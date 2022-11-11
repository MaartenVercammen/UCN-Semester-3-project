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
        public string PictureURL { get; set; }
        public int Time { get; set; }
        public int PortionNum { get; set; }
        public User Author { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }

        public Recipe(Guid recipeId, string name, string description, string pictureURL, int time, int portionNum, User author)
        {
            this.RecipeId = recipeId;
            this.Name = name;
            this.Description = description;
            this.PictureURL = pictureURL;
            this.Time = time;
            this.PortionNum = portionNum;
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
            this.Author = author;
        }

        public Recipe(string name, string description, string pictureURL, int time, int portionNum, User author)
        {
            this.RecipeId = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.PictureURL = pictureURL;
            this.Time = time;
            this.PortionNum = portionNum;
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
            this.Author = author;
        }

        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class Recipe
    {
        public Guid RecipeId { get; set; }
        [MinLength(5), MaxLength(50), Required]
        public string Name { get; set; }
        [MinLength(5), Required]
        public string Description { get; set; }
        [RegularExpression("http(s*)://(.*)[.png, .web, .jpeg .jpg]"), Required]
        public string PictureURL { get; set; }
        [Range(1, int.MaxValue), Required]
        public int Time { get; set; }
        [Range(1, int.MaxValue), Required]
        public int PortionNum { get; set; }
        [Required]
        public User Author { get; set; }
        [MinLength(1), Required]
        public List<Ingredient> Ingredients { get; set; }
        [MinLength(1), Required]
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
            Name = "";
            Description = "";
            PictureURL = "http://placeholder.png";
            Author = new User();
        }

    }
}
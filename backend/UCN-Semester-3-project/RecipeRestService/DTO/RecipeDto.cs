using RecipesData.Model;

namespace RecipeRestService.DTO
{
    public class RecipeDto
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public int Time { get; set; }
        public int PortionNum { get; set; }
        public Guid Author { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }

        public RecipeDto(Guid recipeId, string name, string description, string pictureURL, int time, int portionNum, Guid author)
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

        public RecipeDto(string name, string description, string pictureURL, int time, int portionNum, Guid author)
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

        public RecipeDto()
        {
            this.Ingredients = new List<Ingredient>();
            this.Instructions = new List<Instruction>();
            this.Name = "";
            this.Description = "";
            this.PictureURL = "";
        }

    }
}


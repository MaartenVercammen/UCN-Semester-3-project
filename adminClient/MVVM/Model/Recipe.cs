namespace adminClient.MVVM.Model
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public int Time { get; set; }
        public int PortionNum { get; set; }
        public string Author { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }

        public Recipe(string recipeId, string name, string description, string pictureURL, int time, int portionNum, string author)
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

        public Recipe(string name, string description, string pictureURL, int time, int portionNum, string author)
        {
            this.RecipeId = Guid.NewGuid().ToString();
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
            this.Name = "";
            this.Description = "";
            this.PictureURL = "";
        }

    }
}



using adminClient.Services;

namespace adminClient.MVVM.Model
{
    public class BambooSession
    {
        public string SessionId {get;set;}
        public string Address {get;set;}
        public Recipe RecipeObject {get;set;}

        public string Recipe { get => RecipeObject.RecipeId; set
            {
                var task2 = GetRecipe(value, new RecipeService());
                task2.Wait();
                RecipeObject = task2.Result;
            } }
        public string Description {get;set;}
        public DateTime DateTime {get;set;}
        public int SlotsNumber {get;set;}
        public User HostObject {get; set;}
        public string Host
        {
            get => HostObject.UserId.ToString(); set
            {
                var task = GetHost(value, new UserService());
                task.Wait();
                HostObject = task.Result;
            }
        }
        public List<Seat> Seats {get;set;}


        public BambooSession() { }

        private async Task<User> GetHost(string id, UserService userService)
        {
            var host = await userService.GetUser(id);
            return host;
        }

        private async Task<Recipe> GetRecipe(string id, RecipeService recipeService)
        {
            var recipe = await recipeService.GetRecipe(id);
            return recipe;
        }


    }
}

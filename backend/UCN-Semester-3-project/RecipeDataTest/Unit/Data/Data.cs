using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeDataTest.Unit.Data{

public static class Data
{
    //user
    public static readonly User _validUser = new User("email", "mark", "mark", "password", "home", Role.USER);
    public static readonly User _inValidUser = new User("email", "mark", "mark", "wrong", "home", Role.USER);
    
    //recipe
    public static readonly Recipe _recipe = new Recipe("bananan", "just a fruit", "http://banana.png", 10, 5, _validUser);
    public static readonly DateTime _dateTime = new DateTime(2021, 10, 10, 10, 10, 10);

    // BambooSession
    public static readonly BambooSession _validBambooSession = new BambooSession(Guid.Parse("00000000-0000-0000-0000-000000000000"), _validUser, "address"
    , _recipe, "desc", _dateTime, 4);

    public static readonly List<BambooSession> _ListofBambooSessions = new List<BambooSession>
    {
        _validBambooSession,
        _validBambooSession,
        _validBambooSession
    };

    //SwipedRecipe
    public static readonly SwipedRecipe _swipedRecipeLiked = new SwipedRecipe(_validUser.UserId, _recipe.RecipeId, true);
    public static readonly SwipedRecipe _swipedRecipeDisliked = new SwipedRecipe(_validUser.UserId, _recipe.RecipeId, false);

    public static readonly SwipedRecipeDto _SwipedRecipeLikedDto = new SwipedRecipeDto(_validUser.UserId, _recipe.RecipeId,
        true);
    
    public static readonly List<SwipedRecipe> _SwipedRecipes = new List<SwipedRecipe>()
    {
        _swipedRecipeLiked,
        _swipedRecipeDisliked,
        _swipedRecipeLiked,
        _swipedRecipeDisliked,
        _swipedRecipeLiked,
        _swipedRecipeDisliked
    };
}
}
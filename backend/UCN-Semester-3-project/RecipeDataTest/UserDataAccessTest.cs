using Xunit.Abstractions;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest
{
    public class UserDataAccessTest
    {

        private readonly ITestOutputHelper extraOutput;
        readonly private IUserAccess _userAccess;
        readonly string _connectionString = "data Source=foodpanda.dev,1400; Database=ucn; User Id=dev;Password=dev;";
        public UserDataAccessTest(ITestOutputHelper output)
        {
            this.extraOutput = output;
            _userAccess = new UserDatabaseAccess(_connectionString);
            //IRecipeAccess recipeAccess = new RecipeDatabaseAccess(_connectionString);
        }


        [Fact]
        public void TestGetUserById()
        {
            Guid id = new Guid("00000000-0000-0000-0000-000000000000");
            User user = null;
            user = _userAccess.GetUserById(id);
            Assert.NotNull(user);
        }

    }
}
using Moq;
using RecipeRestService.Businesslogic;
using RecipesData.Database;
using RecipesData.Model;
using Xunit.Abstractions;

namespace RecipeDataTest.BusinessLogic{
    public class BambosessionTest{

        private readonly ITestOutputHelper _extraOutput;

        private readonly BambooSessionDataControl _sut;

        private readonly Mock<IBambooSessionAccess> _acces = new Mock<IBambooSessionAccess>();

        private readonly BambooSession _validBambosession;

        private readonly Recipe _validRecipe;

        private readonly Guid _id;

        private readonly List<BambooSession> _ListofBambooSessions;

        public BambosessionTest(){
            _sut = new BambooSessionDataControl(_acces.Object);
            _id = Guid.NewGuid();

            var validUser = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"),  "mail", "mark", "mark", "pass",
                "street", Role.USER);
            var validIngredient = new Ingredient("banana", 5, "kg");
            var validInstruction = new Instruction(1, "peel the banana");
            _validRecipe = new Recipe( "Banana-bread", "best banana bread in the world",
                "http://picture.png", 30, 4, validUser);
            _validRecipe.Ingredients.Add(validIngredient);
            _validRecipe.Instructions.Add(validInstruction);

            _validBambosession = new BambooSession(_id, validUser, "My home", _validRecipe, "Come to my lovely place and have a nice candel lit dinner", DateTime.Now, 2);

                _ListofBambooSessions     = new List<BambooSession>{
                _validBambosession,
                _validBambosession,
                _validBambosession
            };
            
        }
    
    
        [Fact]
        public void Get_WhenValidId_ReturnsBamboosession()
        {
            //Arrange
            _acces.Setup(x => x.GetBambooSession(_id))
            .Returns(_validBambosession);
            
            //Act

            var response = _sut.Get(_id);
          
            //Assert
            Assert.NotNull(response);
            Assert.Equal(_validBambosession.SessionId, response.SessionId);

        }

        [Fact]
        public void Get_WhenthrowsException_ReturnsNull()
        {
            //Arrange
            _acces.Setup(x => x.GetBambooSession(_id))
            .Throws(new Exception());
            
            //Act

            var response = _sut.Get(_id);
          
            //Assert
            Assert.Null(response);
         
        }

        [Fact]
        public void Get_ReturnsBamboosession()
        {
            //Arrange

            _acces.Setup(x => x.GetBambooSessions())
            .Returns(_ListofBambooSessions);
            
            //Act

            var response = _sut.Get();
          
            //Assert
            Assert.NotNull(response);
            Assert.Equal(_ListofBambooSessions.Count, response.Count);

        }

        [Fact]
        public void Get_WhenThrownException_ReturnsNull()
        {
            //Arrange
            _acces.Setup(x => x.GetBambooSessions())
            .Throws(new Exception());
            
            //Act

            var response = _sut.Get();
          
            //Assert
            Assert.Null(response);
         
        }
    }
}
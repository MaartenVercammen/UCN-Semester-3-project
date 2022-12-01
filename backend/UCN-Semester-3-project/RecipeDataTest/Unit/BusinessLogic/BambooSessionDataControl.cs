using Moq;
using RecipesData.Database;
using RecipesData.Model;
using Xunit.Abstractions;
using RecipeRestService.Businesslogic;

namespace RecipeDataTest.BusinessLogic
{
    public class BambooSessionDataControlTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly BambooSessionDataControl _sut;
        private readonly Mock<IBambooSessionAccess> _acces = new Mock<IBambooSessionAccess>();

        private readonly User _user;
        private readonly Recipe _recipe;
        private readonly DateTime _dateTime;

        // Valid object
        private readonly BambooSession _validBambooSession;

        public BambooSessionDataControlTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _sut = new BambooSessionDataControl(_acces.Object);
            _user = new User("mail", "mark", "mark", "password","recipe street 3200 Diest" ,Role.USER);
            _recipe = new Recipe("bananan", "just a fruit", "http://banana.png", 10, 5, _user);
            _dateTime = new DateTime(2021, 10, 10, 10, 10, 10);
            // Valid object
            _validBambooSession = new BambooSession(Guid.Parse("00000000-0000-0000-0000-000000000000"), _user, "address", _recipe, "desc", _dateTime, 4);
        }

        [Fact]
        public void Add_WhenValidBambooSession_ReturnsGuid()
        {
            //Arrange
            Guid id = new Guid();
            BambooSession inBambooSession = _validBambooSession;
            inBambooSession.SessionId = id;
            _acces.Setup(x => x.CreateBambooSession(inBambooSession))
                .Returns(inBambooSession.SessionId);
            //Act
            var bambooSessionId = _sut.Add(inBambooSession);
            //Assert
            Assert.NotNull(bambooSessionId);
            Assert.Equal(inBambooSession.SessionId, bambooSessionId);
        }

        [Fact]
        public void Add_WhenBambooSessionAndExceptionIsThrown_ReturnsEmptyGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            BambooSession inBambooSession = _validBambooSession;
            inBambooSession.SessionId = id;
            _acces.Setup(x => x.CreateBambooSession(inBambooSession))
                .Throws(new Exception());
            //Act
            Guid bambooSessionId = _sut.Add(inBambooSession);
            //Assert
            Assert.Equal(Guid.Empty, bambooSessionId);
        }
    
    }
}
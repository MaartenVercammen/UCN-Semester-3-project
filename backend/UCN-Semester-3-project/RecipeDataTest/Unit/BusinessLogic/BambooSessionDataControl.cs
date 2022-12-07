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
        private readonly BambooSession _validBambooSession;
        private readonly List<BambooSession> _ListofBambooSessions;

        public BambooSessionDataControlTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _sut = new BambooSessionDataControl(_acces.Object);
            _user = new User("mail", "mark", "mark", "password","recipe street 3200 Diest" ,Role.USER);
            _recipe = new Recipe("bananan", "just a fruit", "http://banana.png", 10, 5, _user);
            _dateTime = new DateTime(2021, 10, 10, 10, 10, 10);
            // Valid object
            _validBambooSession = new BambooSession(Guid.Parse("00000000-0000-0000-0000-000000000000"), _user, "address", _recipe, "desc", _dateTime, 4);
            _ListofBambooSessions = new List<BambooSession>
            {
                _validBambooSession,
                _validBambooSession,
                _validBambooSession
            };
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

        [Fact]
        public void Get_WhenValidId_ReturnsBamboosession()
        {
            //Arrange
            _acces.Setup(x => x.GetBambooSession(_validBambooSession.SessionId))
                .Returns(_validBambooSession);

            //Act

            var response = _sut.Get(_validBambooSession.SessionId);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(_validBambooSession.SessionId, response.SessionId);

        }

        [Fact]
        public void Get_WhenthrowsException_ReturnsNull()
        {
            //Arrange
            _acces.Setup(x => x.GetBambooSession(_validBambooSession.SessionId))
                .Throws(new Exception());

            //Act

            var response = _sut.Get(_validBambooSession.SessionId);

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
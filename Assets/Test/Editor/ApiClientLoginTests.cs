using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor
{
    public class ApiClientLoginTests
    {
        [Test]
        public async Task Login_WithValidCredentials_SetToken()
        {
            // Arrange
            ApiClientLogin apiClientLogin = new ApiClientLogin();
            IText statusMessage = Substitute.For<IText>();
            IText email = Substitute.For<IText>();
            email.text.Returns("unit@test.nl");
            IText password = Substitute.For<IText>();
            password.text.Returns("ABCabc123!@#");
            IApiUtil apiUtil = Substitute.For<IApiUtil>();
            apiUtil.PerformApiCall("https://localhost:7244/account/login", "Post",
                "{\"email\":\"unit@test.nl\",\"password\":\"ABCabc123!@#\"}").Returns(Task.FromResult("{\"tokenType\":\"Bearer\",\"accessToken\":\"TestToken\",\"expiresIn\":3600,\"refreshToken\":\"TestToken\"}"));
            IUserSingleton userSingleton = Substitute.For<IUserSingleton>();
            
            // Act
            await apiClientLogin.Login(statusMessage, email, password, apiUtil, userSingleton);
            
            // Assert
            
            Assert.AreEqual("unit@test.nl", userSingleton.Name);
            Assert.AreEqual("TestToken", userSingleton.AccessToken);
        }
        
        [Test]
        public async Task Register_WithValidCredentials_Successful()
        {
            // Arrange
            ApiClientLogin apiClientLogin = new ApiClientLogin();
            IText statusMessage = Substitute.For<IText>();
            IText email = Substitute.For<IText>();
            email.text.Returns("unit@test.nl");
            IText password = Substitute.For<IText>();
            password.text.Returns("ABCabc123!@#");
            IApiUtil apiUtil = Substitute.For<IApiUtil>();
            apiUtil.PerformApiCall("https://localhost:7244/account/register", "Post",
                "{\"email\":\"unit@test.nl\",\"password\":\"ABCabc123!@#\"}").Returns(Task.FromResult("{\"tokenType\":\"Bearer\",\"accessToken\":\"TestToken\",\"expiresIn\":3600,\"refreshToken\":\"TestToken\"}"));
            // Act
            await apiClientLogin.Register(statusMessage, email, password, apiUtil);
            
            // Assert
            Assert.AreEqual("Registered successfully!", statusMessage.text);
        }
    }
}
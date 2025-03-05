using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace Test.Editor
{
    public class ApiClientRoomCreationBehaviourTests
    {
        [Test]
        public async Task CreateRoom_ValidParameters_Created()
        {
            // Arrange
            ApiClientRoomCreation apiClientRoomCreation = new ApiClientRoomCreation();
            
            IText statusMessage = Substitute.For<IText>();
            IText roomNameInputField = Substitute.For<IText>();
            roomNameInputField.text.Returns("name");
            IText roomHeightInputField = Substitute.For<IText>();
            roomHeightInputField.text.Returns("20");
            IText roomWidthInputField = Substitute.For<IText>();
            roomWidthInputField.text.Returns("20");
            IUserSingleton userSingleton = Substitute.For<IUserSingleton>();
            userSingleton.Token.Returns("TestToken");
            IApiUtil apiUtil = Substitute.For<IApiUtil>();
            apiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Post",
                "{\"name\":\"name\",\"width\":20,\"height\":20,\"tileId\":\"Parquet1\"}", "TestToken").Returns(Task.FromResult(""));
            apiUtil.PerformApiCall("https://localhost:7244/api/Rooms", "Get", token: "TestToken").Returns(Task.FromResult("[{\"roomId\":\"00000000-0000-0000-0000-000000000000\",\"name\":\"123456789123456789123456789\",\"width\":20,\"height\":20,\"tileId\":\"Parquet1\",\"isOwner\":true,\"props\":[]}]"));
            
            // Act
            await apiClientRoomCreation.CreateRoom(apiUtil, statusMessage, roomNameInputField, roomHeightInputField,
                roomWidthInputField, "Parquet1", userSingleton);
            
            // Assert
            Assert.AreEqual("Room created!", statusMessage.text);
        }
    }
}
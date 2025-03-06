using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor
{
    public class ApiClientEnvironmentBuilderBehaviourTests
    {
        [Test]
        public async Task Save_ValidProps_Created()
        {
            // Arrange
            ApiClientEnvironmentBuilder apiClientEnvironmentBuilder = new ApiClientEnvironmentBuilder();
            IGameManager gameManager = Substitute.For<IGameManager>();
            gameManager.Props.Returns(new List<GameObject>());
            IUserSingleton userSingleton = Substitute.For<IUserSingleton>();
            userSingleton.AccessToken.Returns("TestToken");
            IApiUtil apiUtil = Substitute.For<IApiUtil>();
            apiUtil.PerformApiCall($"https://localhost:7244/api/Props?roomId=00000000-0000-0000-0000-000000000000", "DELETE", token: "TestToken").Returns(Task.FromResult(""));
            IText statusText = Substitute.For<IText>();
            // Act
            
            await apiClientEnvironmentBuilder.Save(gameManager, apiUtil, statusText, Guid.Empty, userSingleton);
            // Assert
            Assert.AreEqual("Saved!", statusText.text);
        }
    }
}
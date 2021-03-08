using System;
using System.Linq;
using System.Linq.Expressions;
using ManagementSystem.Data.Models;
using MemberManagementSystem.Controllers;
using Moq;
using NUnit.Framework;

namespace ManagementSystem.Tests
{
    [TestFixture]
    internal class UserTests: BaseTests
    {
        [Test]
        public void GetAllUsers_WhenRequested_ShouldGiveTheUserList()
        {
            // Arrange
            UowMock.Setup(m => 
                    m.Users.Get(It.IsAny<Expression<Func<User, bool>> >(),
                        null, It.IsAny<string>()))
                .Returns(Users);
            
            var controller = new UserManagementController(UowMock.Object, Mapper.Object);
            
            // Act
            var result = controller.GetAllUsersFromUow();
            
            // Assert
            AssertAllCommonPropertiesInResponse(result);

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(5).Items);
                Assert.That(result, Has.Count.EqualTo(5));
            });
            
            CollectionAssert.IsNotEmpty (result);
            CollectionAssert.AllItemsAreNotNull (result);
            CollectionAssert.AllItemsAreUnique (result);
        }
        
        [Test]
        [Ignore("Repository need to be update")]
        public void GetUserById_WhenRequested_ShouldGiveTheUser()
        {
            // Arrange
            UowMock.Setup(m => 
                    m.Users.Get(It.IsAny<Expression<Func<User, bool>> >(),
                        null, It.IsAny<string>()).FirstOrDefault())
                .Returns(Users.FirstOrDefault());
            
            var controller = new UserManagementController(UowMock.Object, Mapper.Object);
            
            // Act
            var result = controller.GetUserFromUow(It.IsAny<int>());
            
            // Assert
            AssertAllCommonPropertiesInResponse(result);

            Assert.That(result, Is.TypeOf<User>());

        }
        
        private void AssertAllCommonPropertiesInResponse<T>(T response) where T: class
        {
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.Not.Empty);
            });

            /*CollectionAssert.IsNotEmpty (response);
            CollectionAssert.AllItemsAreNotNull (response.Data.Weather);
            CollectionAssert.AllItemsAreUnique (response.Data.Weather);*/
        }
    }
}
using System.Collections.Generic;
using AutoMapper;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;
using Moq;
using NUnit.Framework;

namespace ManagementSystem.Tests
{
    
        internal class BaseTests
        {
            protected Mock<IUnitOfWork> UowMock;
            protected Mock<IMapper> Mapper;
            protected List<User> Users;
        
            [SetUp]
            public void BaseSetUp()
            {
                // Arrange
                Users = PopulateUserData();
                Mapper = new Mock<IMapper>();
                UowMock = new Mock<IUnitOfWork>();
            }
        
            [TearDown]
            public void BaseTearDown() { /* ... */ }

            private  List<User> PopulateUserData()
            {
                var list = new List<User>
                {
                    new User
                    {
                        UserId = 1,
                        UserName = "Ubaid",
                        Address = "Homburger Landstr. 207, 60435, Frankfurt am Main, Germany",
                    },
                    new User
                    {
                        UserId = 2,
                        UserName = "Sibgha",
                        Address = "Hombuger str. 290, 60435, Frankfurt am Main, Germany"
                    },

                    new User
                    {
                        UserId = 3,
                        UserName = "Ali",
                        Address = "Nieder Ramstadter str. 64285, Darmstadt, Germany"
                    },
                    new User
                    {
                        UserId = 4,
                        UserName = "Umer",
                        Address = "Am Karlshof. 64285, Darmstadt, Germany"
                    },

                    new User
                    {
                        UserId = 5,
                        UserName = "Florian",
                        Address = "Weisener str. 64287, Darmstadt, Germany"
                    }
                };

                return list;

            }
    }
}
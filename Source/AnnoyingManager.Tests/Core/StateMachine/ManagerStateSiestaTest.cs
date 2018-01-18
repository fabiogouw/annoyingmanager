using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnnoyingManager.Core.StateMachine;
using Moq;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Common;

namespace AnnoyingManager.Tests.Core.StateMachine
{
    public class ManagerStateSiestaTest
    {
        [TestClass]
        public class HandleUnitTest
        {
            [TestMethod]
            public void ShouldGoToAskingTaskAfterSiestaTimeEnds()
            {
                // Arrange
                var mockConfigRep = new Mock<IReadOnlyConfigRepository>();
                var dates = new[] { DateTime.Parse("2015-12-25 13:37"), DateTime.Parse("2015-12-25 13:38") };
                int i = 0;
                mockConfigRep.Setup(m => m.GetCurrentDateTime()).Returns(() => dates[i++]);
                var context = new StateContext()
                {
                    Config = new Config()
                    {
                        SiestaLengthInSeconds = 60
                    },
                    Rested = false
                };
                var state = new ManagerStateSiesta(mockConfigRep.Object);
                // Act
                context = state.Handle(context);
                // Assert
                Assert.AreEqual(StateType.Waiting, context.NewState.Value);
            }

            [TestMethod]
            public void ShouldSend50PercentAsRemainingTimeForSiesta()
            {
                // Arrange
                var mockTaskSupplier = new Mock<ITaskSupplier>();
                var mockConfigRep = new Mock<IReadOnlyConfigRepository>();
                var dates = new[] { DateTime.Parse("2015-12-25 13:37"), DateTime.Parse("2015-12-25 13:38") };
                int i = 0;
                mockConfigRep.Setup(m => m.GetCurrentDateTime()).Returns(() => dates[i++]);
                var context = new StateContext()
                {
                    Config = new Config()
                    {
                        SiestaLengthInSeconds = 120
                    },
                    Rested = false,
                    TaskSupplier = mockTaskSupplier.Object
                };
                var state = new ManagerStateSiesta(mockConfigRep.Object);
                // Act
                context = state.Handle(context);
                // Assert
                mockTaskSupplier.Verify(m => m.UpdateStatus(It.Is<Alert>(a => a.RemainingPercentage == 0.5)), Times.Once());
            }
        }
    }
}

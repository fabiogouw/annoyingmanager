using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnnoyingManager.Core.StateMachine;
using Moq;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Common;
using System.Collections.Generic;

namespace AnnoyingManager.Tests.Core.StateMachine
{
    public class ManagerStateWaitingTest
    {
        [TestClass]
        public class HandleUnitTest
        {
            //[TestMethod]
            //public void ShouldGoToSiestaStateAfterATaskHasFinished()
            //{
            //    // Arrange
            //    var mockConfig = new Mock<IReadOnlyConfigRepository>();
            //    var mockList = new Mock<IDiaryTasksList>();
            //    var context = new StateContext()
            //    {
            //        Config = new Config()
            //        {
            //            SiestaLengthInSeconds = 1
            //        },
            //        Rested = false,
            //        TasksOfTheDay = mockList.Object
            //    };
            //    var state = new ManagerStateWaiting(mockConfig.Object);
            //    // Act
            //    context = state.Handle(context);
            //    // Assert
            //    Assert.AreEqual(StateType.Siesta, context.NewState.Value);
            //}
        }
    }
}

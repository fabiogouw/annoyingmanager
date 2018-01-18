using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnnoyingManager.Core.StateMachine;
using Moq;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;

namespace AnnoyingManager.Tests.Core.StateMachine
{
    public class ManagerStateAskingTaskTest
    {
        [TestClass]
        public class HandleUnitTest
        {
            [TestMethod]
            public void ShouldNotAskANewTaskIfOneIsAlreadyAvailable()
            {
                // Arrange
                var mockTaskSupplier = new Mock<ITaskSupplier>();
                mockTaskSupplier.Setup(m => m.GetTask()).Returns(new Task());
                var context = new StateContext()
                { 
                    TaskSupplier = mockTaskSupplier.Object
                };
                var state = new ManagerStateAskingTask();
                // Act
                state.Handle(context);
                // Assert
                mockTaskSupplier.Verify(m => m.AskForNewTask(It.IsAny<Suggestion>()), Times.Never());
            }
        }
    }
}

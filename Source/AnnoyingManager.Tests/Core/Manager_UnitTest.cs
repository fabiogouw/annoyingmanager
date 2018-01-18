using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnnoyingManager.Core;
using Moq;
using System.Threading;
using AnnoyingManager.Core.Repository;

namespace AnnoyingManager.Tests.Core
{
    [TestClass]
    public class Manager_UnitTest
    {
        //[TestMethod]
        //public void ShouldAskForTaskInfoWhenTheresNoCurrentTask()
        //{
        //    // Arrange
        //    var mockRep = new Mock<ITaskRepository>();
        //    mockRep.Setup(m => m.GetCurrentTasks(It.IsAny<DateTime>())).Returns(new List<Task>());
        //    mockRep.Setup(m => m.GetCurrentDate()).Returns(DateTime.Parse("2012-01-01"));
        //    var mockSup = new Mock<ITaskSupplier>();
        //    Manager target = new Manager() { TaskRepository = mockRep.Object, TaskSupplier = mockSup.Object };
        //    var po = new PrivateObject(target);
        //    // Act
        //    po.Invoke("CheckActionToDo");
        //    // Assert
        //    mockSup.Verify(m => m.AskForNewTask(It.IsAny<Suggestion>()), Times.AtLeastOnce());
        //}
    }
}

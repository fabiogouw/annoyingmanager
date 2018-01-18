using AnnoyingManager.Core;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Tests.Core.Entities
{
    [TestClass]
    public class DiaryTasksListTests
    {
        private Mock<IReadOnlyConfigRepository> _mockConfig;
        private Config _config;

        [TestInitialize]
        public void Initialize()
        {
            _config = new Config()
            {
                StartupTime = TimeSpan.Parse("08:00:00"),
                MaxLengthOfTaskInSeconds = 600  // 10 minutes
            };
            _mockConfig = new Mock<IReadOnlyConfigRepository>();
            _mockConfig.Setup(m => m.GetConfig()).Returns(_config);
            _mockConfig.Setup(p => p.GetCurrentDateTime()).Returns(DateTime.Parse("2015-01-01 10:00"));
        }

        private DiaryTasksList CreateSut()
        {
            var sut = DiaryTasksList.Create(new List<Task>(), _mockConfig.Object);
            return sut;
        }

        private void SetCurrentTime(string time)
        {
            _mockConfig.Setup(p => p.GetCurrentDateTime()).Returns(DateTime.Parse(time));
        }

        [TestMethod]
        public void ShouldAddNewTaskToEmptyListSettingStartTimeAsStartupTime()
        {
            // Arrange
            var sut = CreateSut();
            // Act
            sut.Add(new Task());
            // Assert
            Assert.AreEqual(_config.StartupTime, sut.First().StartDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldEndFirstTaskWhenAddingTheSecond()
        {
            // Arrange
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 10:00") });
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 10:00") });
            // Assert
            Assert.AreEqual(TimeSpan.Parse("08:10:00"), sut.First().EndDate.TimeOfDay);
            Assert.AreEqual(sut.Last().StartDate.TimeOfDay, sut.First().EndDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldConsiderExpectatedDurationWhenFillingPreviousTasks()
        {
            // Arrange
            SetCurrentTime("2015-01-01 09:00");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 09:00"), ExpectedDurationInSeconds = 15 * 60 });
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 09:00") });
            // Assert
            Assert.AreEqual(TimeSpan.Parse("08:25:00"), sut.Last().StartDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldWarnTheresPreviousTasksToBeSupplied()
        {
            // Arrange
            SetCurrentTime("2015-01-01 09:00");
            var sut = CreateSut();
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 09:00") });
            // Assert
            Assert.IsTrue(sut.PastTasksInNeedOfSupply);
        }

        [TestMethod]
        public void ShouldNotWarnTheresPreviousTasksBecauseLastTaskIsValidForCurrentTime()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:09");
            var sut = CreateSut();
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            // Assert
            Assert.IsFalse(sut.PastTasksInNeedOfSupply);
        }

        [TestMethod]
        public void ShouldNotWarnTheresPreviousTasksBecauseLastTaskIsValidForCurrentTimeWithExtendedDuration()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:25");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:25"), ExpectedDurationInSeconds = 25 * 60 });
            // Assert
            Assert.IsFalse(sut.PastTasksInNeedOfSupply);
        }

        [TestMethod]
        public void ShouldEndFirstTaskWhenTheSecondStarts()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:05");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });    // valid until 08:10
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:05") });
            // Assert
            Assert.AreEqual(TimeSpan.Parse("08:05:00"), sut.First().EndDate.TimeOfDay);
            Assert.AreEqual(sut.Last().StartDate.TimeOfDay, sut.First().EndDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldEndLastTaskWithCurrentTimeWhenSavingAll()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:05");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            // Act
            sut.SaveAll();
            // Assert
            Assert.AreEqual(TimeSpan.Parse("08:05:00"), sut.Last().EndDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldStartTaskWithLastEndingTimeAfterSaveAll()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:05");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            sut.SaveAll();
            SetCurrentTime("2015-01-01 08:09");
            // Act
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:09") });
            // Assert
            Assert.AreEqual(TimeSpan.Parse("08:05:00"), sut.First().EndDate.TimeOfDay);
            Assert.AreEqual(TimeSpan.Parse("08:05:00"), sut.Last().StartDate.TimeOfDay);
        }

        [TestMethod]
        public void ShouldNotRepeatLastTaskWhenSavingAllTwice()
        {
            // Arrange
            SetCurrentTime("2015-01-01 08:05");
            var sut = CreateSut();
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:00") });
            sut.Add(new Task() { AssignedDate = DateTime.Parse("2015-01-01 08:09") });
            sut.SaveAll();
            // Act
            sut.SaveAll();
            // Assert
            Assert.AreEqual(2, sut.Count);
        }
    }
}

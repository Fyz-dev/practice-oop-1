using OOP_Lab.Entities;
using OOP_Lab.Enums;

namespace OOP_Lab.Tests
{
    [TestClass]
    public class ApplicationEntitieTests
    {
        [TestMethod]
        public void ApplicationEntitie_Constructor_ShouldCreateObject()
        {
            // Arrange
            var expectedName = "TestApp";
            var expectedDescription = "This is a test app";
            var expectedCategory = ApplicationCategory.Games;
            var expectedRating = 4;
            var expectedVersion = 1;

            // Act
            var appEntity = new ApplicationEntitie(expectedName, expectedDescription, expectedCategory, expectedRating, expectedVersion);

            // Assert
            Assert.AreEqual(expectedName, appEntity.Name);
            Assert.AreEqual(expectedDescription, appEntity.Description);
            Assert.AreEqual(expectedCategory, appEntity.Category);
            Assert.AreEqual(expectedRating, appEntity.Rating);
            Assert.AreEqual(expectedVersion, appEntity.Version);
        }

        [TestMethod]
        public void Name_SetValidValue_ShouldUpdateProperty()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            string newName = "NewTestApp";

            // Act
            appEntity.Name = newName;

            // Assert
            Assert.AreEqual(newName, appEntity.Name);
        }

        [TestMethod]
        public void Name_SetInvalidValue_ShouldThrowArgumentException()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            string invalidName = ""; // Assuming empty names are invalid

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => appEntity.Name = invalidName);
        }

        [TestMethod]
        public void Description_SetValidValue_ShouldUpdateProperty()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            string newDescription = "Updated description";

            // Act
            appEntity.Description = newDescription;

            // Assert
            Assert.AreEqual(newDescription, appEntity.Description);
        }

        [TestMethod]
        public void Description_SetInvalidValue_ShouldThrowArgumentException()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            string invalidDescription = ""; // Assuming empty descriptions are invalid

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => appEntity.Description = invalidDescription);
        }

        [TestMethod]
        public void PopularityScore_ShouldReturnCorrectValue()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 4, 1);
            appEntity.Download();
            appEntity.LastUpdate = DateTime.Now.AddDays(-10);

            // Act
            double actualScore = appEntity.PopularityScore;

            // Assert
            double expectedFreshnessFactor = 1.0;
            double expectedDownloadFactor = Math.Log(appEntity.DownloadCount + 1);
            double expectedRatingFactor = Math.Max(appEntity.Rating, 0) / 5.0;

            double expectedPopularityScore = Math.Round(
                expectedFreshnessFactor * (expectedDownloadFactor * 0.7 + expectedRatingFactor * 0.3),
                1
            );

            Assert.AreEqual(expectedPopularityScore, actualScore);
        }

        [TestMethod]
        public void Update_InstalledApp_ShouldUpdateVersionAndLastUpdate()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            appEntity.Download();

            // Act
            appEntity.Update();

            // Assert
            Assert.AreEqual(2, appEntity.Version);
            Assert.IsTrue((DateTime.Now - appEntity.LastUpdate).TotalSeconds < 1);
        }

        [TestMethod]
        public void Update_NotInstalledApp_ShouldNotUpdateVersionAndLastUpdate()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            var initialVersion = appEntity.Version;
            var initialLastUpdate = appEntity.LastUpdate;

            // Act
            appEntity.Update();

            // Assert
            Assert.AreEqual(initialVersion, appEntity.Version);
            Assert.AreEqual(initialLastUpdate, appEntity.LastUpdate);
        }

        [TestMethod]
        public void Update_OlderVersion_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 2);
            appEntity.Download();

            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() => appEntity.Update(1));
        }

        [TestMethod]
        public void Download_ShouldIncreaseDownloadCountAndSetIsInstalled()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            int initialDownloadCount = appEntity.DownloadCount;

            // Act
            appEntity.Download();

            // Assert
            Assert.AreEqual(initialDownloadCount + 1, appEntity.DownloadCount);
            Assert.IsTrue(appEntity.IsInstalled);
        }

        [TestMethod]
        public void Uninstall_ShouldSetIsInstalledToFalse()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            appEntity.Download(); // Install the app

            // Act
            appEntity.Uninstall();

            // Assert
            Assert.IsFalse(appEntity.IsInstalled);
        }

        [TestMethod]
        public void Delete_ShouldUpdateApplicationCountAndTotalRating()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            int initialApplicationCount = ApplicationEntitie.ApplicationCount;
            double initialTotalRating = ApplicationEntitie.TotalRating;

            // Act
            appEntity.Delete();

            // Assert
            Assert.AreEqual(initialApplicationCount - 1, ApplicationEntitie.ApplicationCount);
            Assert.AreEqual(initialTotalRating - Math.Max(appEntity.Rating, 0), ApplicationEntitie.TotalRating);
        }

        [TestMethod]
        public void AverageRating_NoApplications_ShouldReturnZero()
        {
            // Arrange
            ApplicationEntitie.ApplicationCount = 0;

            // Act
            double averageRating = ApplicationEntitie.AverageRating();

            // Assert
            Assert.AreEqual(0, averageRating);
        }

        [TestMethod]
        public void AverageRating_WithApplications_ShouldReturnFour()
        {
            // Arrange
            ApplicationEntitie.TotalRating = 4;
            ApplicationEntitie.ApplicationCount = 1;

            // Act
            double averageRating = ApplicationEntitie.AverageRating();

            // Assert
            Assert.AreEqual(4, averageRating);
        }

        [TestMethod]
        [DataRow("TestApp;Test Description;Games;4.5;1", "TestApp", "Test Description", ApplicationCategory.Games, 4.5, 1)]
        [DataRow("AnotherApp;Another Description;Other;3.0;2", "AnotherApp", "Another Description", ApplicationCategory.Other, 3.0, 2)]
        [DataRow("SampleApp;Sample Description;Productivity;5.0;3", "SampleApp", "Sample Description", ApplicationCategory.Productivity, 5.0, 3)]
        public void Parse_WithDataRow_ShouldReturnCorrectApplicationEntitie(string inputString, string expectedName, string expectedDescription, ApplicationCategory expectedCategory, double expectedRating, int expectedVersion)
        {
            // Act
            var app = ApplicationEntitie.Parse(inputString);

            // Assert
            Assert.AreEqual(expectedName, app.Name);
            Assert.AreEqual(expectedDescription, app.Description);
            Assert.AreEqual(expectedCategory, app.Category);
            Assert.AreEqual(expectedRating, app.Rating);
            Assert.AreEqual(expectedVersion, app.Version);
        }

        [TestMethod]
        [DataRow("", typeof(ArgumentException))]
        [DataRow("TestApp;Test Description;Other", typeof(FormatException))]
        [DataRow("TestApp+;Test Description;Other;4;1", typeof(ArgumentException))]
        [DataRow("TestApp;;Other;4;1", typeof(ArgumentException))]
        [DataRow("TestApp;Test Description;Test;4;1", typeof(ArgumentException))]
        [DataRow("TestApp;Test Description;Other;-1;1", typeof(ArgumentException))]
        [DataRow("TestApp;Test Description;Other;1;0", typeof(ArgumentException))]
        [DataRow("TestApp+;;0;aaa;0", typeof(ArgumentException))]
        public void Parse_InvalidStrings_ShouldThrowExpectedException(string invalidInput, Type expectedExceptionType)
        {
            // Arrange
            Exception? exception = null;

            // Act
            try
            {
                ApplicationEntitie.Parse(invalidInput);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.IsNotNull(exception, "Expected an exception to be thrown, but none was.");
            Assert.IsInstanceOfType(exception, expectedExceptionType);
        }

        [TestMethod]
        [DataRow("", false)]
        [DataRow("TestApp;Test Description;Other", false)]
        [DataRow("TestApp+;Test Description;Other;4;1", false)]
        [DataRow("TestApp;;Other;4;1", false)]
        [DataRow("TestApp;Test Description;Test;4;1", false)]
        [DataRow("TestApp;Test Description;Other;-1;1", false)]
        [DataRow("TestApp;Test Description;Other;1;0", false)]
        [DataRow("TestApp+;;0;aaa;0", false)]
        [DataRow("TestApp;Test Description;Games;4;1", true)]
        public void TryParse_WithDataRow_ShouldReturnExpectedResult(string inputString, bool expectedResult)
        {
            // Act
            bool result = ApplicationEntitie.TryParse(inputString, out ApplicationEntitie app);

            // Assert
            Assert.AreEqual(expectedResult, result);

            if (expectedResult)
                Assert.IsNotNull(app);
            else
                Assert.IsNull(app);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var appEntity = new ApplicationEntitie("TestApp", "This is a test app", ApplicationCategory.Games, 3.5, 1);
            string expectedString = "TestApp;This is a test app;Games;3.5;1";

            // Act
            string resultString = appEntity.ToString();

            // Assert
            Assert.AreEqual(expectedString, resultString);
        }
    }
}

using OOP_Lab.Entities;
using OOP_Lab.Enums;

namespace OOP_Lab.Tests
{
    [TestClass]
    public class SerializationHelperTests
    {
        private const string TestCsvFilePath = "test.csv";
        private const string TestJsonFilePath = "test.json";

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(TestCsvFilePath))
                File.Delete(TestCsvFilePath);

            if (File.Exists(TestJsonFilePath))
                File.Delete(TestJsonFilePath);
        }

        [TestMethod]
        [DynamicData(nameof(GetDynamicApplications), DynamicDataSourceType.Method)]
        public void SerializeToCsv_TestCase(List<ApplicationEntitie> applications)
        {
            // Act
            SerializationHelper.SerializeToCsv(applications, TestCsvFilePath);

            // Assert
            Assert.IsTrue(File.Exists(TestCsvFilePath), "CSV file was not created.");

            var lines = File.ReadAllLines(TestCsvFilePath);

            Assert.AreEqual(applications.Count, lines.Length, "Expected lines in the CSV file.");

            for (int i = 0; i < applications.Count; i++)
            {
                Assert.AreEqual(
                    applications[i].ToString(),
                    lines[i],
                    "Serialized data does not match the expected data."
                );
            }
        }

        [TestMethod]
        [DynamicData(nameof(GetDynamicApplications), DynamicDataSourceType.Method)]
        public void DeserializeFromCsv_TestCase(List<ApplicationEntitie> applications)
        {
            // Arrange
            SerializationHelper.SerializeToCsv(applications, TestCsvFilePath);

            // Act
            var deserializedApplications = SerializationHelper.DeserializeFromCsv(TestCsvFilePath);

            // Assert
            Assert.AreEqual(
                applications.Count,
                deserializedApplications.Count,
                "Expected multiple applications to be deserialized."
            );

            for (int i = 0; i < applications.Count; i++)
            {
                Assert.AreEqual(
                    applications[i].ToString(),
                    deserializedApplications[i].ToString(),
                    "Deserialized application does not match the expected data."
                );
            }
        }

        [TestMethod]
        public void DeserializeFromCsv_InvalidData_TestCase()
        {
            // Arrange
            var invalidCsvData = "Invalid Data\nMore Invalid Data";
            File.WriteAllText(TestCsvFilePath, invalidCsvData);

            // Act
            var deserializedApplications = SerializationHelper.DeserializeFromCsv(TestCsvFilePath);

            // Assert
            Assert.IsNotNull(deserializedApplications, "Deserialized applications should not be null.");
            Assert.AreEqual(0, deserializedApplications.Count, "Expected no applications to be deserialized from invalid data.");
        }
        public static IEnumerable<object[]> GetDynamicApplications()
        {
            yield return new object[]
            {
                new List<ApplicationEntitie>
                {
                    new ApplicationEntitie(
                        "App1",
                        "Description1",
                        ApplicationCategory.Games,
                        4,
                        1
                    ),
                },
            };

            yield return new object[]
            {
                new List<ApplicationEntitie>
                {
                    new ApplicationEntitie(
                        "App2",
                        "Description2",
                        ApplicationCategory.Productivity,
                        3,
                        1
                    ),
                    new ApplicationEntitie(
                        "App3",
                        "Description3",
                        ApplicationCategory.Games,
                        5,
                        2
                    ),
                },
            };

            yield return new object[]
            {
                new List<ApplicationEntitie>
                {
                    new ApplicationEntitie(
                        "App4",
                        "Description4",
                        ApplicationCategory.Social,
                        4,
                        3
                    ),
                    new ApplicationEntitie(
                        "App5",
                        "Description5",
                        ApplicationCategory.Other,
                        2,
                        4
                    ),
                    new ApplicationEntitie(
                        "App6",
                        "Description6",
                        ApplicationCategory.Games,
                        3,
                        5
                    ),
                    new ApplicationEntitie(
                        "App7",
                        "Description7",
                        ApplicationCategory.Productivity,
                        4,
                        6
                    ),
                },
            };
        }
    }
}

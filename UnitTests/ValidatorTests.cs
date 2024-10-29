using OOP_Lab.Helpers;


namespace OOP_Lab.Tests
{
    [TestClass]
    public class ValidatorTests
    {

        [TestMethod]
        [DataRow(null, "Name is required.")]
        [DataRow("", "Name is required.")]
        [DataRow(" ", "Name is required.")]
        [DataRow("ValidName", null)]
        [DataRow("Invalid_Name!", "Name can only contain letters, numbers, '-', and '_',\nand must be between 1 and 16 characters long.")]
        [DataRow("TooLongName123567", "Name can only contain letters, numbers, '-', and '_',\nand must be between 1 and 16 characters long.")]
        [DataRow("N", null)]
        public void ValidateName_TestCases(string name, string expected)
        {
            var result = Validator.ValidateName(name);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(null, "Description is required.")]
        [DataRow("", "Description is required.")]
        [DataRow("Valid description.", null)]
        [DataRow("Tooooooo long description that exceeds the maximum limit of 230 characters, which is meant to validate the validation logic of the description input. It should provide an appropriate error message when the length exceeds the limit.",
            "Description cannot exceed 230 characters and must not contain the ';' character.")]
        [DataRow("Invalid;Description", "Description cannot exceed 230 characters and must not contain the ';' character.")]
        public void ValidateDescription_TestCases(string description, string expected)
        {
            var result = Validator.ValidateDescription(description);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(null, "Version is required.")]
        [DataRow("", "Version is required.")]
        [DataRow("0", "Version must be a positive integer greater than 0.")]
        [DataRow("-1", "Version must be a positive integer greater than 0.")]
        [DataRow("1", null)]
        [DataRow("abc", "Version must be a positive integer greater than 0.")]
        public void ValidateVersion_TestCases(string version, string expected)
        {
            var result = Validator.ValidateVersion(version);
            Assert.AreEqual(expected, result);
        }
    }
}

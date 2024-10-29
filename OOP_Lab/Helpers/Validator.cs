using System.Text.RegularExpressions;

namespace OOP_Lab.Helpers
{
    public static class Validator
    {
        public static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name is required.";

            var nameRegex = new Regex(@"^[a-zA-Z0-9-_]{1,16}$");
            return !nameRegex.IsMatch(name) ? "Name can only contain letters, numbers, '-', and '_',\nand must be between 1 and 16 characters long." : null;
        }

        public static string ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return "Description is required.";

            var descriptionRegex = new Regex(@"^[^;]{0,230}$");
            return !descriptionRegex.IsMatch(description) ? "Description cannot exceed 230 characters and must not contain the ';' character." : null;
        }

        public static string ValidateVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return "Version is required.";

            if (!int.TryParse(version, out int result) || result <= 0)
                return "Version must be a positive integer greater than 0.";

            return null;
        }
    }
}

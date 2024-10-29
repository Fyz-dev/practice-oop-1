using Microsoft.UI.Xaml;
using OOP_Lab.Helpers;

namespace OOP_Lab.Tests
{
    [TestClass]
    public class BooleanToVisibilityConverterTests
    {
        private readonly BooleanToVisibilityConverter _converter = new BooleanToVisibilityConverter();

        [TestMethod]
        [DataRow(true, Visibility.Visible, false)]
        [DataRow(false, Visibility.Collapsed, false)]
        [DataRow(true, Visibility.Collapsed, true)]
        [DataRow(false, Visibility.Visible, true)]
        [DataRow("string", Visibility.Collapsed, false)]
        [DataRow(123, Visibility.Collapsed, false)]
        [DataRow(null, Visibility.Collapsed, false)]
        public void Convert_TestCases(object input, Visibility expected, bool invert)
        {
            var parameter = invert ? "true" : null;
            var result = _converter.Convert(input, null, parameter, null);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(Visibility.Visible, true, false)]
        [DataRow(Visibility.Collapsed, false, false)]
        [DataRow(Visibility.Visible, false, true)]
        [DataRow(Visibility.Collapsed, true, true)]
        [DataRow(null, false, false)]
        [DataRow(123, false, false)]
        [DataRow("string", false, false)] 
        [DataRow(true, false, false)] 
        public void ConvertBack_TestCases(object input, object expected, bool invert)
        {
            var parameter = invert ? "true" : null;
            var result = _converter.ConvertBack(input, null, parameter, null);
            Assert.AreEqual(expected, result);
        }
    }
}

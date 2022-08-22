// Create a Test Class with coverage for your entire repository functionality.

using DeliveryRepo;

namespace DeliveryTests;

[TestClass]
public class Tests
{
    [TestMethod]
    public void SetCorrectOrder()
    {
        // Arrange
        DeliveryOrder order = new DeliveryOrder();
        order.Name = "Avocado Toast";

        // Act
        string expected = "Avocado Toast";
        string actual = order.Name;

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
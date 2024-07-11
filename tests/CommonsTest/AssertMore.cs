namespace CommonsTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;


public static class AssertMore {
    public static void AreNear(
            double expected,
            double actual,
            double nearness = 0.01,
            string message = null
    ) => Assert.IsTrue(
        expected + nearness >= actual
                && expected - nearness <= actual,
        message
    );

    public static void AreNear(
            double expected,
            double actual,
            string message
    ) => AreNear(expected, actual, 0.01, message);
}

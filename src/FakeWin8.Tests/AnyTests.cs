namespace FakeWin8.Tests
{
    using FakeWin8.Conditions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AnyTests
    {
        [TestMethod]
        public void ShouldAlwaysReturnTrueWhenCreatingIsOK()
        {
            var predicate = Any<byte>.IsOK();

            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                Assert.IsTrue(predicate.Invoke(i));
            }
        }
    }
}

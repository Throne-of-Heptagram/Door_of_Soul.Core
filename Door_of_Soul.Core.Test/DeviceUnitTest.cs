using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Door_of_Soul.Core.Test
{
    [TestClass]
    public class DeviceUnitTest
    {
        [TestMethod]
        public void LinkAnswerTestMethod1()
        {
            Device device = new TestDevice();
            Answer answer = new TestAnswer(1);

            Assert.IsFalse(device.IsAnswerLinked(answer.AnswerID));
            Assert.IsTrue(device.LinkAnswer(answer));
            Assert.IsTrue(device.IsAnswerLinked(answer.AnswerID));
            Assert.IsTrue(answer.IsDeviceLinked(device));
        }
        [TestMethod]
        public void UnlinkAnswerTestMethod1()
        {
            Device device = new TestDevice();
            Answer answer = new TestAnswer(1);
            device.LinkAnswer(answer);

            Assert.IsTrue(device.UnlinkAnswer());
            Assert.IsFalse(device.IsAnswerLinked(answer.AnswerID));
            Assert.IsFalse(answer.IsDeviceLinked(device));
        }
    }
}

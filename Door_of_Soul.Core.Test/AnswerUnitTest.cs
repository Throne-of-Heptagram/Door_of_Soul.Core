using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Door_of_Soul.Core.Test
{
    [TestClass]
    public class AnswerUnitTest
    {
        [TestMethod]
        public void LinkDeviceTestMethod1()
        {
            Device device = new TestDevice();
            Answer answer = new TestAnswer(1);

            Assert.IsFalse(answer.IsDeviceLinked(device));
            Assert.IsTrue(answer.LinkDevice(device));
            Assert.IsTrue(device.IsAnswerLinked(answer.AnswerID));
            Assert.IsTrue(answer.IsDeviceLinked(device));
        }
        [TestMethod]
        public void UnlinkDeviceTestMethod1()
        {
            Device device = new TestDevice();
            Answer answer = new TestAnswer(1);
            answer.LinkDevice(device);

            Assert.IsTrue(answer.UnlinkDevice(device));
            Assert.IsFalse(device.IsAnswerLinked(answer.AnswerID));
            Assert.IsFalse(answer.IsDeviceLinked(device));
        }

        [TestMethod]
        public void LinkSoulTestMethod1()
        {
            Answer answer = new TestAnswer(1);
            Soul soul = new TestSoul(1);

            Assert.IsFalse(answer.IsSoulLinked(soul.SoulID));
            Assert.IsTrue(answer.LinkSoul(soul));
            Assert.IsTrue(answer.IsSoulLinked(soul.SoulID));
            Assert.IsTrue(soul.IsAnswerLinked(answer.AnswerID));
        }
        [TestMethod]
        public void UnlinkSoulTestMethod1()
        {
            Answer answer = new TestAnswer(1);
            Soul soul = new TestSoul(1);
            answer.LinkSoul(soul);

            Assert.IsTrue(answer.UnlinkSoul(soul.SoulID));
            Assert.IsFalse(answer.IsSoulLinked(soul.SoulID));
            Assert.IsFalse(soul.IsAnswerLinked(answer.AnswerID));
        }
    }
}

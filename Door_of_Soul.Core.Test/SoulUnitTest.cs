using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Door_of_Soul.Core.Test
{
    [TestClass]
    public class SoulUnitTest
    {
        [TestMethod]
        public void LinkAnswerTestMethod1()
        {
            Answer answer = new TestAnswer(1);
            Soul soul = new TestSoul(1);

            Assert.IsFalse(soul.IsAnswerLinked(answer.AnswerID));
            Assert.IsTrue(soul.LinkAnswer(answer));
            Assert.IsTrue(answer.IsSoulLinked(soul.SoulID));
            Assert.IsTrue(soul.IsAnswerLinked(answer.AnswerID));
        }
        [TestMethod]
        public void UnlinkAnswerTestMethod1()
        {
            Answer answer = new TestAnswer(1);
            Soul soul = new TestSoul(1);
            soul.LinkAnswer(answer);

            Assert.IsTrue(soul.UnlinkAnswer());
            Assert.IsFalse(answer.IsSoulLinked(soul.SoulID));
            Assert.IsFalse(soul.IsAnswerLinked(answer.AnswerID));
        }

        [TestMethod]
        public void LinkAvatarTestMethod1()
        {
            Soul soul = new TestSoul(1);
            Avatar avatar = new TestAvatar(1);

            Assert.IsFalse(soul.IsAvatarLinked(avatar.AvatarID));
            Assert.IsTrue(soul.LinkAvatar(avatar));
            Assert.IsTrue(soul.IsAvatarLinked(avatar.AvatarID));
            Assert.IsTrue(avatar.IsSoulLinked(soul.SoulID));
        }
        [TestMethod]
        public void UnlinkAvatarTestMethod1()
        {
            Soul soul = new TestSoul(1);
            Avatar avatar = new TestAvatar(1);
            soul.LinkAvatar(avatar);

            Assert.IsTrue(soul.UnlinkAvatar(avatar.AvatarID));
            Assert.IsFalse(soul.IsAvatarLinked(avatar.AvatarID));
            Assert.IsFalse(avatar.IsSoulLinked(soul.SoulID));
        }
    }
}

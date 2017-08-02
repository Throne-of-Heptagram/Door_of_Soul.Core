using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Door_of_Soul.Core.Test
{
    [TestClass]
    public class AvatarUnitTest
    {
        [TestMethod]
        public void LinkSoulTestMethod1()
        {
            Soul soul = new TestSoul(1);
            Avatar avatar = new TestAvatar(1);

            Assert.IsFalse(avatar.IsSoulLinked(soul.SoulID));
            Assert.IsTrue(avatar.LinkSoul(soul));
            Assert.IsTrue(avatar.IsSoulLinked(soul.SoulID));
            Assert.IsTrue(soul.IsAvatarLinked(avatar.AvatarID));
        }
        [TestMethod]
        public void UnlinkSoulTestMethod1()
        {
            Soul soul = new TestSoul(1);
            Avatar avatar = new TestAvatar(1);
            avatar.LinkSoul(soul);

            Assert.IsTrue(avatar.UnlinkSoul(soul.SoulID));
            Assert.IsFalse(avatar.IsSoulLinked(soul.SoulID));
            Assert.IsFalse(soul.IsAnswerLinked(avatar.AvatarID));
        }
    }
}

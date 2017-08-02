using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Door_of_Soul.Core
{
    public abstract class Avatar
    {
        public int AvatarID { get; private set; }

        private object soulDictionaryLock = new object();
        private Dictionary<int, Soul> soulDictionary = new Dictionary<int, Soul>();
        public IEnumerable<Soul> Souls
        {
            get
            {
                lock (soulDictionaryLock)
                {
                    return soulDictionary.Values.ToArray();
                }
            }
        }

        public event Action<Avatar, Soul> OnSoulLinked;
        public event Action<Avatar, Soul> OnSoulUnlinked;

        protected Avatar(int avatarID)
        {
            AvatarID = avatarID;
        }

        public bool IsSoulLinked(int soulID)
        {
            return soulDictionary.ContainsKey(soulID);
        }
        public bool LinkSoul(Soul soul)
        {
            lock (soulDictionaryLock)
            {
                if (IsSoulLinked(soul.SoulID))
                {
                    return false;
                }
                else
                {
                    soulDictionary.Add(soul.SoulID, soul);
                    if (!soul.IsAvatarLinked(AvatarID))
                    {
                        soul.LinkAvatar(this);
                    }
                    OnSoulLinked?.Invoke(this, soul);
                    return true;
                }
            }
        }
        public bool UnlinkSoul(int soulID)
        {
            lock (soulDictionaryLock)
            {
                if (IsSoulLinked(soulID))
                {
                    Soul soul = soulDictionary[soulID];
                    soulDictionary.Remove(soulID);
                    if (soul.IsAvatarLinked(AvatarID))
                    {
                        soul.UnlinkAvatar(AvatarID);
                    }
                    OnSoulUnlinked?.Invoke(this, soul);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

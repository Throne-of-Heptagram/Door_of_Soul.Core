using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Avatar
    {
        public event Action<Avatar, Soul> OnSoulLinked;
        public event Action<Avatar, Soul> OnSoulUnlinked;

        public event Action<Avatar, int> OnExistedSceneIdUpdated;

        public int AvatarId { get; private set; }
        public virtual int EntityId { get; private set; }

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

        private int existedSceneId;
        public int ExistedSceneId
        {
            get { return existedSceneId; }
            set
            {
                existedSceneId = value;
                OnExistedSceneIdUpdated?.Invoke(this, existedSceneId);
            }
        }

        protected Avatar(int avatarId)
        {
            AvatarId = avatarId;
        }

        public bool IsSoulLinked(int soulId)
        {
            return soulDictionary.ContainsKey(soulId);
        }
        public bool LinkSoul(Soul soul)
        {
            lock (soulDictionaryLock)
            {
                if (IsSoulLinked(soul.SoulId))
                {
                    return false;
                }
                else
                {
                    soulDictionary.Add(soul.SoulId, soul);
                    if (!soul.IsAvatarLinked(AvatarId))
                    {
                        soul.LinkAvatar(this);
                    }
                    OnSoulLinked?.Invoke(this, soul);
                    return true;
                }
            }
        }
        public bool UnlinkSoul(int soulId)
        {
            lock (soulDictionaryLock)
            {
                if (IsSoulLinked(soulId))
                {
                    Soul soul = soulDictionary[soulId];
                    soulDictionary.Remove(soulId);
                    if (soul.IsAvatarLinked(AvatarId))
                    {
                        soul.UnlinkAvatar(AvatarId);
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

        public bool FindSoul(int soulId, out Soul soul)
        {
            lock (soulDictionaryLock)
            {
                if (IsSoulLinked(soulId))
                {
                    soul = soulDictionary[soulId];
                    return true;
                }
                else
                {
                    soul = null;
                    return false;
                }
            }
        }
    }
}

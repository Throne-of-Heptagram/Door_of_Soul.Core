using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Avatar
    {
        public event Action<Avatar, int> OnSoulLinked;
        public event Action<Avatar, int> OnSoulUnlinked;

        public int AvatarId { get; private set; }
        public int EntityId { get; private set; }
        public string AvatarName { get; private set; }

        private object soulIdSetLock = new object();
        private HashSet<int> soulSet = new HashSet<int>();
        public IEnumerable<int> SoulIds
        {
            get
            {
                lock (soulIdSetLock)
                {
                    return soulSet.ToArray();
                }
            }
        }

        protected Avatar(int avatarId, int entityId, string avatarName)
        {
            AvatarId = avatarId;
            EntityId = entityId;
            AvatarName = avatarName;
        }
        public override string ToString()
        {
            return $"Avatar Id:{AvatarId} EntityId:{EntityId} Name:{AvatarName}";
        }

        public bool IsSoulLinked(int soulId)
        {
            return soulSet.Contains(soulId);
        }
        public bool LinkSoul(int soulId)
        {
            lock (soulIdSetLock)
            {
                if (IsSoulLinked(soulId))
                {
                    return false;
                }
                else
                {
                    soulSet.Add(soulId);
                    OnSoulLinked?.Invoke(this, soulId);
                    return true;
                }
            }
        }
        public bool UnlinkSoul(int soulId)
        {
            lock (soulIdSetLock)
            {
                if (IsSoulLinked(soulId))
                {
                    soulSet.Remove(soulId);
                    OnSoulUnlinked?.Invoke(this, soulId);
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

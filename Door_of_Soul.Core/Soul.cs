﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Soul
    {
        public event Action<Soul, int> OnAnswerLinked;
        public event Action<Soul, int> OnAnswerUnlinked;

        public event Action<Soul, int> OnAvatarLinked;
        public event Action<Soul, int> OnAvatarUnlinked;

        public event Action<Soul> OnActivated;
        public event Action<Soul> OnDeactivated;

        public int SoulId { get; private set; }
        public string SoulName { get; private set; }
        public bool IsActivated { get; private set; }

        private object answerIdLock = new object();
        private int answerId;
        public int AnswerId
        {
            get { return answerId; }
            set
            {
                lock (answerIdLock)
                {
                    if (AnswerId != value)
                    {
                        int originalAnswerId = AnswerId;
                        answerId = value;
                        if (originalAnswerId != 0)
                            OnAnswerUnlinked?.Invoke(this, originalAnswerId);
                        if (AnswerId != 0)
                            OnAnswerLinked?.Invoke(this, AnswerId);
                    }
                }
            }
        }

        private object avatarIdSetLock = new object();
        private HashSet<int> avatarIdSet = new HashSet<int>();
        public IEnumerable<int> AvatarIds
        {
            get
            {
                lock (avatarIdSetLock)
                {
                    return avatarIdSet.ToArray();
                }
            }
        }

        protected Soul(int soulId, string soulName, bool isActivated)
        {
            SoulId = soulId;
            SoulName = soulName;
            IsActivated = isActivated;
        }
        public override string ToString()
        {
            return $"Soul Id:{SoulId} Name:{SoulName} IsActivated:{IsActivated} AnswerId:{AnswerId}";
        }

        public bool IsAvatarLinked(int avatarId)
        {
            return avatarIdSet.Contains(avatarId);
        }
        public bool LinkAvatar(int avatarId)
        {
            lock (avatarIdSetLock)
            {
                if (IsAvatarLinked(avatarId))
                {
                    return false;
                }
                else
                {
                    avatarIdSet.Add(avatarId);
                    OnAvatarLinked?.Invoke(this, avatarId);
                    return true;
                }
            }
        }
        public bool UnlinkAvatar(int avatarId)
        {
            lock (avatarIdSetLock)
            {
                if (IsAvatarLinked(avatarId))
                {
                    avatarIdSet.Remove(avatarId);
                    OnAvatarUnlinked?.Invoke(this, avatarId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Activate()
        {
            IsActivated = true;
            OnActivated?.Invoke(this);
        }
        public void Deactivate()
        {
            IsActivated = false;
            OnDeactivated?.Invoke(this);
        }
    }
}

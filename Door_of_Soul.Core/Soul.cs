using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Soul
    {
        public event Action<Soul, Answer> OnAnswerLinked;
        public event Action<Soul, Answer> OnAnswerUnlinked;

        public event Action<Soul, Avatar> OnAvatarLinked;
        public event Action<Soul, Avatar> OnAvatarUnlinked;

        public int SoulId { get; private set; }
        public Answer Answer { get; private set; }

        private object avatarDictionaryLock = new object();
        private Dictionary<int, Avatar> avatarDictionary = new Dictionary<int, Avatar>();
        public IEnumerable<Avatar> Avatars
        {
            get
            {
                lock (avatarDictionaryLock)
                {
                    return avatarDictionary.Values.ToArray();
                }
            }
        }

        protected Soul(int soulId)
        {
            SoulId = soulId;
        }

        public bool IsAnswerLinked(int answerId)
        {
            return Answer?.AnswerId == answerId;
        }
        public bool LinkAnswer(Answer answer)
        {
            if(Answer != answer)
            {
                if (Answer != null)
                {
                    UnlinkAnswer();
                }
                Answer = answer;
                if (!Answer.IsSoulLinked(SoulId))
                {
                    Answer.LinkSoul(this);
                }
                OnAnswerLinked?.Invoke(this, Answer);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UnlinkAnswer()
        {
            if (Answer != null)
            {
                if (Answer.IsSoulLinked(SoulId))
                {
                    Answer.UnlinkSoul(SoulId);
                }
                Answer answer = Answer;
                Answer = null;
                OnAnswerUnlinked?.Invoke(this, answer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAvatarLinked(int avatarId)
        {
            return avatarDictionary.ContainsKey(avatarId);
        }
        public bool LinkAvatar(Avatar avatar)
        {
            lock (avatarDictionaryLock)
            {
                if (IsAvatarLinked(avatar.AvatarId))
                {
                    return false;
                }
                else
                {
                    avatarDictionary.Add(avatar.AvatarId, avatar);
                    if(!avatar.IsSoulLinked(SoulId))
                    {
                        avatar.LinkSoul(this);
                    }
                    OnAvatarLinked?.Invoke(this, avatar);
                    return true;
                }
            }
        }
        public bool UnlinkAvatar(int avatarId)
        {
            lock (avatarDictionaryLock)
            {
                if (IsAvatarLinked(avatarId))
                {
                    Avatar avatar = avatarDictionary[avatarId];
                    avatarDictionary.Remove(avatarId);
                    if(avatar.IsSoulLinked(SoulId))
                    {
                        avatar.UnlinkSoul(SoulId);
                    }
                    OnAvatarUnlinked?.Invoke(this, avatar);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindAvatar(int avatarId, out Avatar avatar)
        {
            lock (avatarDictionaryLock)
            {
                if (IsAvatarLinked(avatarId))
                {
                    avatar = avatarDictionary[avatarId];
                    return true;
                }
                else
                {
                    avatar = null;
                    return false;
                }
            }
        }
    }
}

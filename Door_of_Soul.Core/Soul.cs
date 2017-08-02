using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Soul
    {
        public int SoulID { get; private set; }
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

        public event Action<Soul> OnAnswerLinked;
        public event Action<Soul> OnAnswerUnlinked;

        public event Action<Soul, Avatar> OnAvatarLinked;
        public event Action<Soul, Avatar> OnAvatarUnlinked;

        protected Soul(int soulID)
        {
            SoulID = soulID;
        }

        public bool IsAnswerLinked(int answerID)
        {
            return Answer != null && Answer.AnswerID == answerID;
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
                if (!Answer.IsSoulLinked(SoulID))
                {
                    Answer.LinkSoul(this);
                }
                OnAnswerLinked?.Invoke(this);
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
                if (Answer.IsSoulLinked(SoulID))
                {
                    Answer.UnlinkSoul(SoulID);
                }
                Answer = null;
                OnAnswerUnlinked?.Invoke(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAvatarLinked(int avatarID)
        {
            return avatarDictionary.ContainsKey(avatarID);
        }
        public bool LinkAvatar(Avatar avatar)
        {
            lock (avatarDictionaryLock)
            {
                if (IsAvatarLinked(avatar.AvatarID))
                {
                    return false;
                }
                else
                {
                    avatarDictionary.Add(avatar.AvatarID, avatar);
                    if(!avatar.IsSoulLinked(SoulID))
                    {
                        avatar.LinkSoul(this);
                    }
                    OnAvatarLinked?.Invoke(this, avatar);
                    return true;
                }
            }
        }
        public bool UnlinkAvatar(int avatarID)
        {
            lock (avatarDictionaryLock)
            {
                if (IsAvatarLinked(avatarID))
                {
                    Avatar avatar = avatarDictionary[avatarID];
                    avatarDictionary.Remove(avatarID);
                    if(avatar.IsSoulLinked(SoulID))
                    {
                        avatar.UnlinkSoul(SoulID);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Answer
    {
        public event Action<Answer, Soul> OnSoulLinked;
        public event Action<Answer, Soul> OnSoulUnlinked;

        public int AnswerId { get; private set; }

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

        protected Answer(int answerId)
        {
            AnswerId = answerId;
        }

        public bool IsSoulLinked(int soulId)
        {
            return soulDictionary.ContainsKey(soulId);
        }
        public bool LinkSoul(Soul soul)
        {
            lock (soulDictionaryLock)
            {
                if(IsSoulLinked(soul.SoulId))
                {
                    return false;
                }
                else
                {
                    soulDictionary.Add(soul.SoulId, soul);
                    if(!soul.IsAnswerLinked(AnswerId))
                    {
                        soul.LinkAnswer(this);
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
                    if(soul.IsAnswerLinked(AnswerId))
                    {
                        soul.UnlinkAnswer();
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
                if(IsSoulLinked(soulId))
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

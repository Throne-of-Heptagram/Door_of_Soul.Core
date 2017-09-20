using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Answer
    {
        public event Action<Answer, int> OnSoulLinked;
        public event Action<Answer, int> OnSoulUnlinked;

        public int AnswerId { get; private set; }
        public string AnswerName { get; private set; }

        private object soulIdSetLock = new object();
        private HashSet<int> soulIdSet = new HashSet<int>();
        public IEnumerable<int> SoulIds
        {
            get
            {
                lock (soulIdSetLock)
                {
                    return soulIdSet.ToArray();
                }
            }
        }

        protected Answer(int answerId, string answerName)
        {
            AnswerId = answerId;
            AnswerName = answerName;
        }

        public bool IsSoulLinked(int soulId)
        {
            return soulIdSet.Contains(soulId);
        }
        public bool LinkSoul(int soulId)
        {
            lock (soulIdSetLock)
            {
                if(IsSoulLinked(soulId))
                {
                    return false;
                }
                else
                {
                    soulIdSet.Add(soulId);
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
                    soulIdSet.Remove(soulId);
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

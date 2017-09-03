using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class GenericSubjectRepository<TSubjectId, TSubject>
    {
        public event Action<TSubject> OnSubjectAdded;
        public event Action<TSubject> OnSubjectRemoved;

        private Dictionary<TSubjectId, TSubject> subjectDictionary = new Dictionary<TSubjectId, TSubject>();
        private object subjectDictionaryLock = new object();
        public IEnumerable<TSubject> Subjects
        {
            get
            {
                lock (subjectDictionaryLock)
                {
                    return subjectDictionary.Values.ToArray();
                }
            }
        }

        public bool Contains(TSubjectId subjectId)
        {
            return subjectDictionary.ContainsKey(subjectId);
        }

        public bool Add(TSubjectId subjectId, TSubject subject)
        {
            lock (subjectDictionaryLock)
            {
                if (Contains(subjectId))
                {
                    return false;
                }
                else
                {
                    subjectDictionary.Add(subjectId, subject);
                    OnSubjectAdded?.Invoke(subject);
                    return true;
                }
            }
        }
        public bool Find(TSubjectId subjectId, out TSubject subject)
        {
            lock (subjectDictionaryLock)
            {
                if (Contains(subjectId))
                {
                    subject = subjectDictionary[subjectId];
                    return true;
                }
                else
                {
                    subject = default(TSubject);
                    return false;
                }
            }
        }
        public bool Remove(TSubjectId subjectId)
        {
            lock (subjectDictionaryLock)
            {
                if (Contains(subjectId))
                {
                    TSubject subject = subjectDictionary[subjectId];
                    subjectDictionary.Remove(subjectId);
                    OnSubjectRemoved?.Invoke(subject);
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

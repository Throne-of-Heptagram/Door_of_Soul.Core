using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Answer
    {
        public int AnswerID { get; private set; }

        private object devicesLock = new object();
        private List<Device> devices = new List<Device>();
        public IEnumerable<Device> Devices
        {
            get
            {
                lock(devicesLock)
                {
                    return devices.ToArray();
                }
            }
        }

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

        public event Action<Answer, Device> OnDeviceLinked;
        public event Action<Answer, Device> OnDeviceUnlinked;

        public event Action<Answer, Soul> OnSoulLinked;
        public event Action<Answer, Soul> OnSoulUnlinked;

        protected Answer(int answerID)
        {
            AnswerID = answerID;
        }

        public bool IsDeviceLinked(Device device)
        {
            return devices.Contains(device);
        }
        public bool LinkDevice(Device device)
        {
            lock (devicesLock)
            {
                if(IsDeviceLinked(device))
                {
                    return false;
                }
                else
                {
                    devices.Add(device);
                    if(!device.IsAnswerLinked(AnswerID))
                    {
                        device.LinkAnswer(this);
                    }
                    OnDeviceLinked?.Invoke(this, device);
                    return true;
                }
            }
        }
        public bool UnlinkDevice(Device device)
        {
            lock (devicesLock)
            {
                if (!IsDeviceLinked(device))
                {
                    return false;
                }
                else
                {
                    if (devices.Remove(device))
                    {
                        if(device.IsAnswerLinked(AnswerID))
                        {
                            device.UnlinkAnswer();
                        }
                        OnDeviceUnlinked?.Invoke(this, device);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool IsSoulLinked(int soulID)
        {
            return soulDictionary.ContainsKey(soulID);
        }
        public bool LinkSoul(Soul soul)
        {
            lock (soulDictionaryLock)
            {
                if(IsSoulLinked(soul.SoulID))
                {
                    return false;
                }
                else
                {
                    soulDictionary.Add(soul.SoulID, soul);
                    if(!soul.IsAnswerLinked(AnswerID))
                    {
                        soul.LinkAnswer(this);
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
                    if(soul.IsAnswerLinked(AnswerID))
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
    }
}

using System;
using System.Collections.Generic;

namespace Door_of_Soul.Core
{
    public class DisposableEvent<TSender, TEventParameter>
    {
        private TSender subject;

        private int eventHandlerIdCounter = 0;
        private object eventHandlerLock = new object();
        private Dictionary<int, Func<TSender, TEventParameter, bool>> eventHandlerDictionary = new Dictionary<int, Func<TSender, TEventParameter, bool>>();


        public DisposableEvent(TSender subject)
        {
            this.subject = subject;
        }

        public int RegisterEvent(Func<TSender, TEventParameter, bool> eventHandler, List<IEventDependencyReleasable> dependentTargets)
        {
            lock (eventHandlerLock)
            {
                int eventHandlerId = eventHandlerIdCounter++;
                eventHandlerDictionary.Add(eventHandlerId, eventHandler);
                foreach(var target in dependentTargets)
                {
                    int targetEventHandlerId = eventHandlerId;
                    target.OnEventDependencyDisappear += () => UnregisterEventById(targetEventHandlerId);
                }
                return eventHandlerId;
            }
        }

        public void UnregisterEventById(int eventHandlerId)
        {
            lock (eventHandlerLock)
            {
                if(eventHandlerDictionary.ContainsKey(eventHandlerId))
                {
                    Func<TSender, TEventParameter, bool> eventHandler = eventHandlerDictionary[eventHandlerId];
                    eventHandlerDictionary.Remove(eventHandlerId);
                }
            }
        }

        public void InvokeEvent(TEventParameter eventParameter)
        {
            lock(eventHandlerLock)
            {
                List<int> removableEventHandlerIds = new List<int>();
                foreach(var pair in eventHandlerDictionary)
                {
                    if(pair.Value.Invoke(subject, eventParameter))
                    {
                        removableEventHandlerIds.Add(pair.Key);
                    }
                }
                foreach(var eventHandlerId in removableEventHandlerIds)
                {
                    eventHandlerDictionary.Remove(eventHandlerId);
                }
            }
        }
    }
}

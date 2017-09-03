using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class World
    {
        public event Action<World, int> OnSceneAdded;
        public event Action<World, int> OnSceneRemoved;

        public int WorldId { get; private set; }

        private object sceneIdSetLock = new object();
        private HashSet<int> sceneIdSet = new HashSet<int>();
        public IEnumerable<int> SceneIds
        {
            get
            {
                lock (sceneIdSetLock)
                {
                    return sceneIdSet.ToArray();
                }
            }
        }

        public bool IsSceneExisted(int sceneId)
        {
            return sceneIdSet.Contains(sceneId);
        }
        public bool AddScene(int sceneId)
        {
            lock (sceneIdSetLock)
            {
                if (IsSceneExisted(sceneId))
                {
                    return false;
                }
                else
                {
                    sceneIdSet.Add(sceneId);
                    OnSceneAdded?.Invoke(this, sceneId);
                    return true;
                }
            }
        }
        public bool RemoveScene(int sceneId)
        {
            lock (sceneIdSetLock)
            {
                if (IsSceneExisted(sceneId))
                {
                    sceneIdSet.Remove(sceneId);
                    OnSceneRemoved?.Invoke(this, sceneId);
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

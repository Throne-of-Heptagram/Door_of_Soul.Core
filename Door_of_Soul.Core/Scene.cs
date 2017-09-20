using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Scene
    {
        public event Action<Scene, int> OnWorldLinked;
        public event Action<Scene, int> OnWorldUnlinked;

        public event Action<Scene, int> OnEntityEntered;
        public event Action<Scene, int> OnEntityExited;

        public event Action<Scene, int> OnObserverAvatarLinked;
        public event Action<Scene, int> OnObserverAvatarUnlinked;

        public int SceneId { get; private set; }

        private object worldIdLock = new object();
        private int worldId;
        public int WorldId
        {
            get { return worldId; }
            set
            {
                lock (worldIdLock)
                {
                    if (WorldId != value)
                    {
                        int originalWorldId = WorldId;
                        worldId = value;
                        if (originalWorldId != 0)
                            OnWorldUnlinked?.Invoke(this, originalWorldId);
                        if (WorldId != 0)
                            OnWorldLinked?.Invoke(this, WorldId);
                    }
                }
            }
        }

        private object observerAvatarIdLock = new object();
        private int observerAvatarId;
        public int ObserverAvatarId
        {
            get { return observerAvatarId; }
            set
            {
                lock (observerAvatarIdLock)
                {
                    if (ObserverAvatarId != value)
                    {
                        int originalObserverAvatarId = ObserverAvatarId;
                        observerAvatarId = value;
                        if (originalObserverAvatarId != 0)
                            OnObserverAvatarUnlinked?.Invoke(this, originalObserverAvatarId);
                        if (ObserverAvatarId != 0)
                            OnObserverAvatarLinked?.Invoke(this, ObserverAvatarId);
                    }
                }
            }
        }

        private object entityIdSetLock = new object();
        private HashSet<int> entityIdSet = new HashSet<int>();
        public IEnumerable<int> EntityIds
        {
            get
            {
                lock (entityIdSetLock)
                {
                    return entityIdSet.ToArray();
                }
            }
        }

        public bool IsEntityExisted(int entityId)
        {
            return entityIdSet.Contains(entityId);
        }
        public bool EntityEnter(int entityId)
        {
            lock (entityIdSetLock)
            {
                if (IsEntityExisted(entityId))
                {
                    return false;
                }
                else
                {
                    entityIdSet.Add(entityId);
                    OnEntityEntered?.Invoke(this, entityId);
                    return true;
                }
            }
        }
        public bool EntityExit(int entityId)
        {
            lock (entityIdSetLock)
            {
                if (IsEntityExisted(entityId))
                {
                    entityIdSet.Remove(entityId);
                    OnEntityExited?.Invoke(this, entityId);
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

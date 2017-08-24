using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class Scene
    {
        public event Action<Scene, World> OnWorldLinked;
        public event Action<Scene, World> OnWorldUnlinked;

        public event Action<Scene, Entity> OnEntityEntered;
        public event Action<Scene, Entity> OnEntityExited;

        public event Action<Scene, int> OnObserverAvatarIdUpdated;

        public int SceneId { get; private set; }
        public World BelongingWorld { get; private set; }
        public string SceneServerName { get; private set; }
        private int observerAvatarId;
        public int ObserverAvatarId
        {
            get { return observerAvatarId; }
            set
            {
                observerAvatarId = value;
                OnObserverAvatarIdUpdated?.Invoke(this, observerAvatarId);
            }
        }

        private object entityDictionaryLock = new object();
        private Dictionary<int, Entity> entityDictionary = new Dictionary<int, Entity>();
        public IEnumerable<Entity> Entities
        {
            get
            {
                lock (entityDictionaryLock)
                {
                    return entityDictionary.Values.ToArray();
                }
            }
        }

        public bool IsWorldLinked(int worldId)
        {
            return BelongingWorld != null && BelongingWorld.WorldId == worldId;
        }
        public bool LinkWorld(World world)
        {
            if (BelongingWorld != world)
            {
                UnlinkWorld();
                BelongingWorld = world;
                if (!BelongingWorld.IsSceneExisted(SceneId))
                {
                    BelongingWorld.AddScene(this);
                }
                OnWorldLinked?.Invoke(this, world);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UnlinkWorld()
        {
            if (BelongingWorld != null)
            {
                if (BelongingWorld.IsSceneExisted(SceneId))
                {
                    BelongingWorld.RemoveScene(SceneId);
                }
                World world = BelongingWorld;
                BelongingWorld = null;
                OnWorldUnlinked?.Invoke(this, world);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEntityExisted(int entityId)
        {
            return entityDictionary.ContainsKey(entityId);
        }
        public bool EntityEnter(Entity entity)
        {
            lock (entityDictionaryLock)
            {
                if (IsEntityExisted(entity.EntityId))
                {
                    return false;
                }
                else
                {
                    entityDictionary.Add(entity.EntityId, entity);
                    if (!entity.IsExistedInScene(SceneId))
                    {
                        entity.EnterScene(this);
                    }
                    OnEntityEntered?.Invoke(this, entity);
                    return true;
                }
            }
        }
        public bool EntityExit(int entityId)
        {
            lock (entityDictionaryLock)
            {
                if (IsEntityExisted(entityId))
                {
                    Entity entity = entityDictionary[entityId];
                    entityDictionary.Remove(entityId);
                    if (entity.IsExistedInScene(SceneId))
                    {
                        entity.ExitScene();
                    }
                    OnEntityExited?.Invoke(this, entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindEntity(int entityId, out Entity entity)
        {
            lock (entityDictionaryLock)
            {
                if (IsEntityExisted(entityId))
                {
                    entity = entityDictionary[entityId];
                    return true;
                }
                else
                {
                    entity = null;
                    return false;
                }
            }
        }
    }
}

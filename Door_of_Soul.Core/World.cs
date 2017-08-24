using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Core
{
    public abstract class World
    {
        public event Action<World, Scene> OnSceneAdded;
        public event Action<World, Scene> OnSceneRemoved;

        public int WorldId { get; private set; }

        private object sceneDictionaryLock = new object();
        private Dictionary<int, Scene> sceneDictionary = new Dictionary<int, Scene>();
        public IEnumerable<Scene> Scenes
        {
            get
            {
                lock (sceneDictionaryLock)
                {
                    return sceneDictionary.Values.ToArray();
                }
            }
        }

        public bool IsSceneExisted(int sceneId)
        {
            return sceneDictionary.ContainsKey(sceneId);
        }
        public bool AddScene(Scene scene)
        {
            lock (sceneDictionaryLock)
            {
                if (IsSceneExisted(scene.SceneId))
                {
                    return false;
                }
                else
                {
                    sceneDictionary.Add(scene.SceneId, scene);
                    if (!scene.IsWorldLinked(WorldId))
                    {
                        scene.LinkWorld(this);
                    }
                    OnSceneAdded?.Invoke(this, scene);
                    return true;
                }
            }
        }
        public bool RemoveScene(int sceneId)
        {
            lock (sceneDictionaryLock)
            {
                if (IsSceneExisted(sceneId))
                {
                    Scene scene = sceneDictionary[sceneId];
                    sceneDictionary.Remove(sceneId);
                    if (scene.IsWorldLinked(WorldId))
                    {
                        scene.UnlinkWorld();
                    }
                    OnSceneRemoved?.Invoke(this, scene);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool FindScene(int sceneId, out Scene scene)
        {
            lock (sceneDictionaryLock)
            {
                if (IsSceneExisted(sceneId))
                {
                    scene = sceneDictionary[sceneId];
                    return true;
                }
                else
                {
                    scene = null;
                    return false;
                }
            }
        }
    }
}

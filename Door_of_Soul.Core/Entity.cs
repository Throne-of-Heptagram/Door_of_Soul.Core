using System;

namespace Door_of_Soul.Core
{
    public abstract class Entity
    {
        public event Action<Entity, Scene> OnEnteredScene;
        public event Action<Entity, Scene> OnExitedScene;

        public int EntityId { get; private set; }
        private object existedSceneLock = new object();
        public Scene ExistedScene { get; private set; }

        public bool IsExistedInScene(int sceneId)
        {
            return ExistedScene?.SceneId == sceneId;
        }
        public bool EnterScene(Scene scene)
        {
            lock (existedSceneLock)
            {
                if (IsExistedInScene(scene.SceneId))
                {
                    return false;
                }
                else
                {
                    ExitScene();
                    ExistedScene = scene;
                    if (!scene.IsEntityExisted(EntityId))
                    {
                        scene.EntityEnter(this);
                    }
                    OnEnteredScene?.Invoke(this, ExistedScene);
                    return true;
                }
            }
        }
        public bool ExitScene()
        {
            lock (existedSceneLock)
            {
                if (ExistedScene != null)
                {
                    Scene scene = ExistedScene;
                    ExistedScene = null;
                    if (scene.IsEntityExisted(EntityId))
                    {
                        scene.EntityExit(EntityId);
                    }
                    OnExitedScene?.Invoke(this, scene);
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

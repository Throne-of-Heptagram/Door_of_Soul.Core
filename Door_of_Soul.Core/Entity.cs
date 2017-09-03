using System;

namespace Door_of_Soul.Core
{
    public abstract class Entity
    {
        public event Action<Entity, int> OnEnteredScene;
        public event Action<Entity, int> OnExitedScene;

        public int EntityId { get; private set; }
        private object existedSceneIdLock = new object();
        private int existedSceneId;
        public int ExistedSceneId
        {
            get { return existedSceneId; }
            private set
            {
                lock (existedSceneIdLock)
                {
                    if (ExistedSceneId != value)
                    {
                        int originalSceneId = ExistedSceneId;
                        ExistedSceneId = value;
                        if(originalSceneId != 0)
                            OnExitedScene?.Invoke(this, originalSceneId);
                        if (ExistedSceneId != 0)
                            OnEnteredScene?.Invoke(this, ExistedSceneId);
                    }
                }
            }
        }
    }
}

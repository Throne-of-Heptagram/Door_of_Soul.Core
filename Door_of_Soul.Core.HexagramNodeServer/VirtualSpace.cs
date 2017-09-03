namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualSpace
    {
        public static VirtualSpace Instance { get; private set; }
        public static void Initial(VirtualSpace instance)
        {
            Instance = instance;
        }

        public abstract bool FindWorld(int worldId, out VirtualWorld world);
        public abstract bool FindScene(int sceneId, out VirtualScene scene);
    }
}

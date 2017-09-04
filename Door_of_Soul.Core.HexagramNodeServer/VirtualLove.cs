namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualLove
    {
        public static VirtualLove Instance { get; private set; }
        public static void Initialize(VirtualLove instance)
        {
            Instance = instance;
        }
    }
}

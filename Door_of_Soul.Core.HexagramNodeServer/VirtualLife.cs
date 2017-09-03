namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualLife
    {
        public static VirtualLife Instance { get; private set; }
        public static void Initial(VirtualLife instance)
        {
            Instance = instance;
        }
    }
}

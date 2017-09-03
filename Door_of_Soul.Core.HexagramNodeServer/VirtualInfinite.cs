namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualInfinite
    {
        public static VirtualInfinite Instance { get; private set; }
        public static void Initial(VirtualInfinite instance)
        {
            Instance = instance;
        }
    }
}

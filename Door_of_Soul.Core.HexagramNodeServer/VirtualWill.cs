namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualWill
    {
        public static VirtualWill Instance { get; private set; }
        public static void Initial(VirtualWill instance)
        {
            Instance = instance;
        }
    }
}

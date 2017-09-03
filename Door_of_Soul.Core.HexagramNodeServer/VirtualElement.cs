namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualElement
    {
        public static VirtualElement Instance { get; private set; }
        public static void Initial(VirtualElement instance)
        {
            Instance = instance;
        }
    }
}

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualEternity
    {
        public static VirtualEternity Instance { get; private set; }
        public static void Initial(VirtualEternity instance)
        {
            Instance = instance;
        }
    }
}

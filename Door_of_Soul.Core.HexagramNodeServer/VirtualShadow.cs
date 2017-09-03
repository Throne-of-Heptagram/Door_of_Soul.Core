namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualShadow
    {
        public static VirtualShadow Instance { get; private set; }
        public static void Initial(VirtualShadow instance)
        {
            Instance = instance;
        }
    }
}

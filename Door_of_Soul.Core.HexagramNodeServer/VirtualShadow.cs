namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualShadow
    {
        public static VirtualShadow Instance { get; private set; }
        public static void Initialize(VirtualShadow instance)
        {
            Instance = instance;
        }
    }
}

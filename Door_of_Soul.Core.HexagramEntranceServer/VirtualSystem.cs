namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initial(VirtualSystem instance)
        {
            Instance = instance;
        }
    }
}

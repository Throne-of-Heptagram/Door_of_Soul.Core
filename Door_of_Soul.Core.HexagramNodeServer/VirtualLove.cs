namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualLove
    {
        public static VirtualLove Instance { get; private set; }
        public static void Initial(VirtualLove instance)
        {
            Instance = instance;
        }
    }
}

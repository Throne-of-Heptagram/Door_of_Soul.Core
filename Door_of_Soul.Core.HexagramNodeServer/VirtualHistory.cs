namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualHistory
    {
        public static VirtualHistory Instance { get; private set; }
        public static void Initialize(VirtualHistory instance)
        {
            Instance = instance;
        }
    }
}

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualElement
    {
        public static VirtualElement Instance { get; private set; }
        public static void Initialize(VirtualElement instance)
        {
            Instance = instance;
        }

        public override string ToString()
        {
            return "Element";
        }
    }
}

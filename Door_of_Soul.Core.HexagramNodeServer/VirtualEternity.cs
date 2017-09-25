namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualEternity
    {
        public static VirtualEternity Instance { get; private set; }
        public static void Initialize(VirtualEternity instance)
        {
            Instance = instance;
        }

        public override string ToString()
        {
            return "Eternity";
        }
    }
}

using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualWill
    {
        public static VirtualWill Instance { get; private set; }
        public static void Initialize(VirtualWill instance)
        {
            Instance = instance;
        }
    }
}

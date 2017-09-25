using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualLife
    {
        public static VirtualLife Instance { get; private set; }
        public static void Initialize(VirtualLife instance)
        {
            Instance = instance;
        }

        public override string ToString()
        {
            return "Life";
        }
    }
}

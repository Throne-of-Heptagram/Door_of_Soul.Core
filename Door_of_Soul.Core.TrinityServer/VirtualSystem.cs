using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.TrinityServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode AssignAnswer(int answerId, out string errorMessage);
    }
}

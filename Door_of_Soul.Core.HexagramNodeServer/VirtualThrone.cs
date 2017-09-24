using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualThrone
    {
        public static VirtualThrone Instance { get; private set; }
        public static void Initialize(VirtualThrone instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode DeviceRegister(int entranceId, int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage);
    }
}

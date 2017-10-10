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

        public delegate void GetAnswerTrinityServerResponseEventHandler(OperationReturnCode returnCode, string operationMessage, int trinityServerEndPointId, int answerId, string answerAccessToken);
        public abstract event GetAnswerTrinityServerResponseEventHandler OnGetAnswerTrinityServer;

        public override string ToString()
        {
            return "Throne";
        }

        public abstract OperationReturnCode DeviceRegister(int entranceId, int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage);

        public abstract OperationReturnCode GetAnswerTrinityServer(int entranceId, int answerId, out string errorMessage);
        public abstract void GetAnswerTrinityServerResponse(OperationReturnCode returnCode, string operationMessage, int trinityServerEndPointId, int answerId, string answerAccessToken);
    }
}

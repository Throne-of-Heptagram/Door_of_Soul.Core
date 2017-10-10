using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.LoginServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public delegate void GetAnswerTrinityServerResponseEventHandler(OperationReturnCode returnCode, string operationMessage, int trinityServerEndPointId, int answerId, string answerAccessToken);
        public abstract event GetAnswerTrinityServerResponseEventHandler OnGetAnswerTrinityServer;

        public abstract OperationReturnCode Register(int deviceId, string answerName, string basicPassword, out string errorMessage);

        public abstract OperationReturnCode Login(int deviceId, string answerName, string basicPassword, out string errorMessage);

        public abstract OperationReturnCode GetAnswerTrinityServer(int answerId, out string errorMessage);
        public abstract void GetAnswerTrinityServerResponse(OperationReturnCode returnCode, string operationMessage, int trinityServerEndPointId, int answerId, string answerAccessToken);
    }
}

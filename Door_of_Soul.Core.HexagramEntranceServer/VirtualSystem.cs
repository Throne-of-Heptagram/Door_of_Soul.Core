using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public struct GetAnswerTrinityServerResponseParameter
        {
            public OperationReturnCode returnCode;
            public string operationMessage;
            public int trinityServerEndPointId;
            public int answerId;
            public string answerAccessToken;
        }
        protected DisposableEvent<VirtualSystem, GetAnswerTrinityServerResponseParameter> OnGetAnswerTrinityServer { get; private set; }


        public struct AssignAnswerResponseParameter
        {
            public OperationReturnCode returnCode;
            public string operationMessage;
            public int trinityServerEndPointId;
            public int answerId;
            public string answerAccessToken;
        }
        protected DisposableEvent<VirtualSystem, AssignAnswerResponseParameter> OnAssignAnswer { get; private set; }

        protected VirtualSystem()
        {
            OnGetAnswerTrinityServer = new DisposableEvent<VirtualSystem, GetAnswerTrinityServerResponseParameter>(this);
            OnAssignAnswer = new DisposableEvent<VirtualSystem, AssignAnswerResponseParameter>(this);
        }

        public abstract OperationReturnCode DeviceRegister(int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage);

        public abstract OperationReturnCode GetAnswerTrinityServer(int endPointId, int answerId, out string errorMessage);
        public abstract void GetAnswerTrinityServerResponse(GetAnswerTrinityServerResponseParameter responseParameter);

        public abstract OperationReturnCode AssignAnswer(int answerId, out string errorMessage);
        public abstract void AssignAnswerResponse(AssignAnswerResponseParameter responseParameter);
    }
}

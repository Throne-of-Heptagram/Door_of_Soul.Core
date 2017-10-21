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

        public struct AssignAnswerResponseParameter
        {
            public OperationReturnCode returnCode;
            public string operationMessage;
            public int trinityServerEndPointId;
            public int answerId;
            public string answerAccessToken;
        }
        protected DisposableEvent<VirtualThrone, AssignAnswerResponseParameter> OnAssignAnswer { get; private set; }

        public VirtualThrone()
        {
            OnAssignAnswer = new DisposableEvent<VirtualThrone, AssignAnswerResponseParameter>(this);
        }

        public override string ToString()
        {
            return "Throne";
        }

        public abstract OperationReturnCode DeviceRegister(int entranceId, int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage);

        public abstract OperationReturnCode GetAnswerTrinityServer(int entranceId, int answerId, out string errorMessage);

        public abstract OperationReturnCode AssignAnswer(int answerId, out string errorMessage);
        public abstract void AssignAnswerResponse(AssignAnswerResponseParameter responseParameter);
    }
}

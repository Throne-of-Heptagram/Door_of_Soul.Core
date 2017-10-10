using Door_of_Soul.Core.Protocol;
using System;

namespace Door_of_Soul.Core.Client
{
    public abstract class VirtualSystem : System
    {
        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract event Action<OperationReturnCode, string> OnRegisterResponse;
        public delegate void LoginResponseEventHandler(OperationReturnCode returnCode, string operationMessage, string trinityServerAddress, int trinityServerPort, string trinityServerApplicationName, int answerId, string answerAccessToken);
        public abstract event LoginResponseEventHandler OnLoginResponse;

        public abstract OperationReturnCode Register(string answerName, string basicPassword, out string errorMessage);
        public abstract void RegisterResponse(OperationReturnCode returnCode, string operationMessage);

        public abstract OperationReturnCode Login(string answerName, string basicPassword, out string errorMessage);
        public abstract void LoginResponse(OperationReturnCode returnCode, string operationMessage, string trinityServerAddress, int trinityServerPort, string trinityServerApplicationName, int answerId, string answerAccessToken);
    }
}

using Door_of_Soul.Core.Protocol;
using System;

namespace Door_of_Soul.Core.Client
{
    public abstract class VirtualSystem : System
    {
        public event Action<OperationReturnCode, string> OnRegisterResponse;
        public delegate void LoginResponseEventHandler(OperationReturnCode returnCode, string operationMessage, string trinityServerAddress, int trinityServerPort, string trinityServerApplicationName, int answerId, string answerAccessToken);
        public event LoginResponseEventHandler OnLoginResponse;

        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode Register(string answerName, string basicPassword, out string errorMessage);
        public void RegisterResponse(OperationReturnCode returnCode, string operationMessage)
        {
            OnRegisterResponse?.Invoke(returnCode, operationMessage);
        }

        public abstract OperationReturnCode Login(string answerName, string basicPassword, out string errorMessage);
        public void LoginResponse(OperationReturnCode returnCode, string operationMessage, string trinityServerAddress, int trinityServerPort, string trinityServerApplicationName, int answerId, string answerAccessToken)
        {
            OnLoginResponse?.Invoke(returnCode, operationMessage, trinityServerAddress, trinityServerPort, trinityServerApplicationName, answerId, answerAccessToken);
        }
    }
}

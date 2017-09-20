using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.ProxyServer
{
    public abstract class VirtualSystem : System
    {
        public delegate void GetHexagramEntranceAnswerResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualAnswer answer);
        public event GetHexagramEntranceAnswerResponseEventHandler OnGetHexagramEntranceAnswerResponse;
        protected Dictionary<int, GetHexagramEntranceAnswerResponseEventHandler> getHexagramEntranceAnswerResponseEventHandlerDictionary = new Dictionary<int, GetHexagramEntranceAnswerResponseEventHandler>();
        protected int getHexagramEntranceAnswerResponseEventHandlerCounter = 1;
        protected object onGetHexagramEntranceAnswerResponseEventLock = new object();

        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode Register(int deviceId, string answerName, string basicPassword, out string errorMessage);
        public abstract OperationReturnCode Login(int deviceId, string answerName, string basicPassword, out string errorMessage);
        public abstract OperationReturnCode GetHexagramEntranceAnswer(int deviceId, int answerId, out string errorMessage);
        protected abstract bool InstantiateAnswer(int answerId, string answerName, int[] soulIds, out VirtualAnswer answer);
        public void GetHexagramEntranceAnswerResponse(OperationReturnCode returnCode, string operationMessage, int answerId, string answerName, int[] soulIds)
        {
            lock(onGetHexagramEntranceAnswerResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualAnswer answer;
                    if (InstantiateAnswer(answerId, answerName, soulIds, out answer))
                    {
                        OnGetHexagramEntranceAnswerResponse?.Invoke(returnCode, operationMessage, answer);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetHexagramEntranceAnswerResponse Instantiate Answer Failed";
                        OnGetHexagramEntranceAnswerResponse?.Invoke(returnCode, operationMessage, answer);
                    }
                }
                else
                {
                    OnGetHexagramEntranceAnswerResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

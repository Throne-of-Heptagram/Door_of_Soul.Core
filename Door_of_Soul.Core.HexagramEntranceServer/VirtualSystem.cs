using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualSystem : System
    {
        public delegate void GetThroneAnswerResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualAnswer answer);
        public event GetThroneAnswerResponseEventHandler OnGetThroneAnswerResponse;
        protected Dictionary<int, GetThroneAnswerResponseEventHandler> getThroneAnswerResponseEventHandlerDictionary = new Dictionary<int, GetThroneAnswerResponseEventHandler>();
        protected int getThroneAnswerResponseEventHandlerCounter = 1;
        protected object onGetThroneAnswerResponseEventLock = new object();

        public static VirtualSystem Instance { get; private set; }
        public static void Initialize(VirtualSystem instance)
        {
            Instance = instance;
        }

        public abstract OperationReturnCode GetThroneAnswer(int endPointId, int answerId, out string errorMessage);
        protected abstract bool InstantiateAnswer(int answerId, string answerName, int[] soulIds, out VirtualAnswer answer);
        public void GetThroneAnswerResponse(OperationReturnCode returnCode, string operationMessage, int answerId, string answerName, int[] soulIds)
        {
            lock(onGetThroneAnswerResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualAnswer answer;
                    if (InstantiateAnswer(answerId, answerName, soulIds, out answer))
                    {
                        OnGetThroneAnswerResponse?.Invoke(returnCode, operationMessage, answer);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetThroneAnswerResponse Instantiate Answer Failed";
                        OnGetThroneAnswerResponse?.Invoke(returnCode, operationMessage, answer);
                    }
                }
                else
                {
                    OnGetThroneAnswerResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.ProxyServer
{
    public abstract class VirtualAnswer : Answer
    {
        public delegate void GetHexagramEntranceSoulResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualSoul soul);
        public event GetHexagramEntranceSoulResponseEventHandler OnGetHexagramEntranceSoulResponse;
        protected Dictionary<int, GetHexagramEntranceSoulResponseEventHandler> getHexagramEntranceSoulResponseEventHandlerDictionary = new Dictionary<int, GetHexagramEntranceSoulResponseEventHandler>();
        protected int getHexagramEntranceSoulResponseEventHandlerCounter = 1;
        protected object onGetHexagramEntranceSoulResponseEventLock = new object();

        protected VirtualAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }

        public abstract OperationReturnCode GetHexagramEntranceSoul(int deviceId, int soulId, out string errorMessage);
        protected abstract bool InstantiateSoul(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds, out VirtualSoul soul);
        public void GetHexagramEntranceSoulResponse(OperationReturnCode returnCode, string operationMessage, int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds)
        {
            lock (onGetHexagramEntranceSoulResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualSoul soul;
                    if (InstantiateSoul(soulId, soulName, isActivated, answerId, avatarIds, out soul))
                    {
                        OnGetHexagramEntranceSoulResponse?.Invoke(returnCode, operationMessage, soul);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetHexagramEntranceSoulResponse Instantiate Soul Failed";
                        OnGetHexagramEntranceSoulResponse?.Invoke(returnCode, operationMessage, soul);
                    }
                }
                else
                {
                    OnGetHexagramEntranceSoulResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

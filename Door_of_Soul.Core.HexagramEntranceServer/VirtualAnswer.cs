using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualAnswer : Answer
    {
        public delegate void GetWillSoulResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualSoul soul);
        public event GetWillSoulResponseEventHandler OnGetWillSoulResponse;
        protected Dictionary<int, GetWillSoulResponseEventHandler> getWillSoulResponseEventHandlerDictionary = new Dictionary<int, GetWillSoulResponseEventHandler>();
        protected int getWillSoulResponseEventHandlerCounter = 1;
        protected object onGetWillSoulResponseEventLock = new object();

        protected VirtualAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }

        public abstract OperationReturnCode GetWillSoul(int endPointId, int soulId, out string errorMessage);
        protected abstract bool InstantiateSoul(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds, out VirtualSoul soul);
        public void GetWillSoulResponse(OperationReturnCode returnCode, string operationMessage, int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds)
        {
            lock(onGetWillSoulResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualSoul soul;
                    if (InstantiateSoul(soulId, soulName, isActivated, answerId, avatarIds, out soul))
                    {
                        OnGetWillSoulResponse?.Invoke(returnCode, operationMessage, soul);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetWillSoulResponse Instantiate Soul Failed";
                        OnGetWillSoulResponse?.Invoke(returnCode, operationMessage, soul);
                    }
                }
                else
                {
                    OnGetWillSoulResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.ProxyServer
{
    public abstract class VirtualSoul : Soul
    {
        public delegate void GetHexagramEntranceAvatarResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualAvatar avatar);
        public event GetHexagramEntranceAvatarResponseEventHandler OnGetHexagramEntranceAvatarResponse;
        protected Dictionary<int, GetHexagramEntranceAvatarResponseEventHandler> getHexagramEntranceAvatarResponseEventHandlerDictionary = new Dictionary<int, GetHexagramEntranceAvatarResponseEventHandler>();
        protected int getHexagramEntranceAvatarResponseEventHandlerCounter = 1;
        protected object onGetHexagramEntranceAvatarResponseEventLock = new object();

        protected VirtualSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }

        public abstract OperationReturnCode GetHexagramEntranceAvatar(int deviceId, int avatarId, out string errorMessage);
        protected abstract bool InstantiateAvatar(int avatarId, int entityId, string avatarName, int[] soulIds, out VirtualAvatar avatar);
        public void GetHexagramEntranceAvatarResponse(OperationReturnCode returnCode, string operationMessage, int avatarId, int entityId, string avatarName, int[] soulIds)
        {
            lock (onGetHexagramEntranceAvatarResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualAvatar avatar;
                    if (InstantiateAvatar(avatarId, entityId, avatarName, soulIds, out avatar))
                    {
                        OnGetHexagramEntranceAvatarResponse?.Invoke(returnCode, operationMessage, avatar);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetHexagramEntranceAvatarResponse Instantiate Avatar Failed";
                        OnGetHexagramEntranceAvatarResponse?.Invoke(returnCode, operationMessage, avatar);
                    }
                }
                else
                {
                    OnGetHexagramEntranceAvatarResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

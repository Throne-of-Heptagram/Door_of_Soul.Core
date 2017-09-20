using Door_of_Soul.Core.Protocol;
using System.Collections.Generic;

namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualSoul : Soul
    {
        public delegate void GetLifeAvatarResponseEventHandler(OperationReturnCode returnCode, string operationMessage, VirtualAvatar avatar);
        public event GetLifeAvatarResponseEventHandler OnGetLifeAvatarResponse;
        protected Dictionary<int, GetLifeAvatarResponseEventHandler> getLifeAvatarResponseEventHandlerDictionary = new Dictionary<int, GetLifeAvatarResponseEventHandler>();
        protected int getLifeAvatarResponseEventHandlerCounter = 1;
        protected object onGetLifeAvatarResponseEventLock = new object();

        protected VirtualSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }

        public abstract OperationReturnCode GetLifeAvatar(int endPointId, int avatarId, out string errorMessage);
        protected abstract bool InstantiateAvatar(int avatarId, int entityId, string avatarName, int[] soulIds, out VirtualAvatar avatar);
        public void GetLifeAvatarResponse(OperationReturnCode returnCode, string operationMessage, int avatarId, int entityId, string avatarName, int[] soulIds)
        {
            lock(onGetLifeAvatarResponseEventLock)
            {
                if (returnCode == OperationReturnCode.Successiful)
                {
                    VirtualAvatar avatar;
                    if (InstantiateAvatar(avatarId, entityId, avatarName, soulIds, out avatar))
                    {
                        OnGetLifeAvatarResponse?.Invoke(returnCode, operationMessage, avatar);
                    }
                    else
                    {
                        returnCode = OperationReturnCode.InstantiateFailed;
                        operationMessage = "GetLifeAvatarResponse Instantiate Avatar Failed";
                        OnGetLifeAvatarResponse?.Invoke(returnCode, operationMessage, avatar);
                    }
                }
                else
                {
                    OnGetLifeAvatarResponse?.Invoke(returnCode, operationMessage, null);
                }
            }
        }
    }
}

using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualLife
    {
        public static VirtualLife Instance { get; private set; }
        public static void Initialize(VirtualLife instance)
        {
            Instance = instance;
        }

        protected object getLifeAvatarLock = new object();

        public abstract OperationReturnCode GetLifeAvatar(int hexagramEntranceId, int avatarId, out string errorMessage);
    }
}

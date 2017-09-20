namespace Door_of_Soul.Core.Client
{
    public abstract class VirtualSoul : Soul
    {
        protected VirtualSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }

        public abstract void LoadProxyAvatar(int avatarId, int entityId, string avatarName, int[] soulIds);
    }
}

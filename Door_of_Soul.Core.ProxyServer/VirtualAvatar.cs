namespace Door_of_Soul.Core.ProxyServer
{
    public abstract class VirtualAvatar : Avatar
    {
        protected VirtualAvatar(int avatarId, int entityId, string avatarName) : base(avatarId, entityId, avatarName)
        {
        }
    }
}

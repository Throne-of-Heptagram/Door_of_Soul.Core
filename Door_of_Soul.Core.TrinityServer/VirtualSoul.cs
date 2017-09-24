namespace Door_of_Soul.Core.TrinityServer
{
    public abstract class VirtualSoul : Soul
    {
        protected VirtualSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }
    }
}

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualKnowledge
    {
        public static VirtualKnowledge Instance { get; private set; }
        public static void Initial(VirtualKnowledge instance)
        {
            Instance = instance;
        }
    }
}

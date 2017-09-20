namespace Door_of_Soul.Core.Client
{
    public abstract class VirtualAnswer : Answer
    {
        public static VirtualAnswer Instance { get; private set; }
        public static void Initialize(VirtualAnswer instance)
        {
            Instance = instance;
        }

        protected VirtualAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }

        public abstract void LoadProxySoul(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds);
    }
}

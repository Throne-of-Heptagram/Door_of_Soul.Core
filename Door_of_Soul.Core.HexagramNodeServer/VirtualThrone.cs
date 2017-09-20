using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualThrone
    {
        public static VirtualThrone Instance { get; private set; }
        public static void Initialize(VirtualThrone instance)
        {
            Instance = instance;
        }

        protected object getThroneAnswerLock = new object();

        public abstract OperationReturnCode GetThroneAnswer(int hexagramEntranceId, int answerId, out string errorMessage);
    }
}

namespace Door_of_Soul.Core.HexagramNodeServer
{
    public abstract class VirtualAnswer : Answer
    {
        public int AccessHexagramEntranceId { get; private set; }
        public int AccessEndPointId { get; private set; }
        public string AnswerAccessToken { get; private set; }

        protected VirtualAnswer(int answerId, string answerName, int accessHexagramEntranceId, int accessEndPointId, string answerAccessToken) : base(answerId, answerName)
        {
            AccessHexagramEntranceId = accessHexagramEntranceId;
            AccessEndPointId = accessEndPointId;
            AnswerAccessToken = answerAccessToken;
        }

        public override string ToString()
        {
            return $"{base.ToString()} AccessEndPointId:{AccessEndPointId}";
        }
    }
}

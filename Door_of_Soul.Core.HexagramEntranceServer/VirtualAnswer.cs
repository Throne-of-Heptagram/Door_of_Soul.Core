namespace Door_of_Soul.Core.HexagramEntranceServer
{
    public abstract class VirtualAnswer : Answer
    {
        public int AccessEndPointId { get; private set; }
        public string AnswerAccessToken { get; private set; }

        protected VirtualAnswer(int answerId, string answerName, int accessEndPointId, string answerAccessToken) : base(answerId, answerName)
        {
            AccessEndPointId = accessEndPointId;
            AnswerAccessToken = answerAccessToken;
        }

        public override string ToString()
        {
            return $"{base.ToString()} AccessEndPointId:{AccessEndPointId}";
        }
    }
}

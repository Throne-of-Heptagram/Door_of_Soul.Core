using System;

namespace Door_of_Soul.Core
{
    public abstract class Device
    {
        public Answer Answer { get; private set; }

        public event Action<Device> OnAnswerLinked;
        public event Action<Device> OnAnswerUnlinked;

        protected Device()
        {

        }

        public bool IsAnswerLinked(int answerID)
        {
            return Answer != null && Answer.AnswerID == answerID;
        }
        public bool LinkAnswer(Answer answer)
        {
            if(Answer != answer)
            {
                if(Answer == null)
                {
                    UnlinkAnswer();
                }
                Answer = answer;
                if (!Answer.IsDeviceLinked(this))
                {
                    Answer.LinkDevice(this);
                }
                OnAnswerLinked?.Invoke(this);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UnlinkAnswer()
        {
            if (Answer != null)
            {
                if (Answer.IsDeviceLinked(this))
                {
                    Answer.UnlinkDevice(this);
                }
                Answer = null;
                OnAnswerUnlinked?.Invoke(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
